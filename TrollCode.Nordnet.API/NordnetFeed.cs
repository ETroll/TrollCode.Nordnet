using System;
using System.Collections.Generic;
using System.Text;
using Trollcode.Nordnet.API.Responses;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using Trollcode.Nordnet.API.FeedModels;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Concurrent;

namespace Trollcode.Nordnet.API
{
    public abstract class NordnetFeed : IObservable<FeedResponse>, IDisposable
    {
        /// <summary>
        /// When the SSL Socket has successfully connected
        /// </summary>
        public event EventHandler OnConnected;

        /// <summary>
        /// When the SSL Socket has disconnected
        /// </summary>
        public event EventHandler OnDisconnected;

        private readonly List<IObserver<FeedResponse>> observers = new List<IObserver<FeedResponse>>();
        private readonly ConcurrentQueue<Command> commandQueue = new ConcurrentQueue<Command>();

        private TcpClient tcpClient;
        private SslStream sslStream;

        private Thread txThread;
        private Thread rxThread;

        private bool hostClosedConnection = false;

        /// <summary>
        /// Change the default command waiting time before connecting
        /// </summary>
        public int CommandWaitTime { get; set; } = 100;

        public void SendGenericCommand(Command cmd)
        {
            commandQueue.Enqueue(cmd);
            //sslStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new Command
            //{
            //    //{"market_id":14,"country":"DK","name":"Nasdaq OMX Copenhagen"},
            //    cmd = "subscribe", //"args":{"t":"price", "i":"1869", "m":30}}
            //    args = new Dictionary<string, string>
            //    {
            //        {"t", "price"},
            //        {"i", "1869" },
            //        {"m", "14"}
            //    }
            //}) + "\n"));
            //sslStream.Flush();
        }

        protected void ConnectToFeedAndStart(string hostname, int port, CancellationToken cancellationToken)
        {
            txThread = new Thread(() =>
            {
                tcpClient = new TcpClient(hostname, port);
                sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                {
                    //Dont validate for now. Add later!
                    return true;
                }), null);

                try
                {
                    sslStream.AuthenticateAsClient("I Will Fail Now");
                }
                catch (AuthenticationException e)
                {
                    tcpClient.Close();
                    throw e;
                }
                OnConnected?.Invoke(this, new EventArgs());

                rxThread = new Thread(StartRead);
                rxThread.Start(cancellationToken);

                while (!cancellationToken.IsCancellationRequested && !hostClosedConnection)
                {
                    while (commandQueue.IsEmpty)
                    {
                        Thread.Sleep(CommandWaitTime);
                    }

                    if (commandQueue.TryDequeue(out Command cmd))
                    {
                        sslStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(cmd) + "\n"));
                        sslStream.Flush();
                    }   
                }
                //Clean up!!
                tcpClient.Close();
                foreach (var observer in observers)
                {
                    observer.OnCompleted();
                }
                OnDisconnected?.Invoke(this, new EventArgs());
            });
            txThread.Start();
        }

        private void StartRead(object token)
        {
            CancellationToken cancellationToken = (CancellationToken)token;

            byte[] buffer = new byte[2048];
            int bytes;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                //Decoder decoder = Encoding.ASCII.GetDecoder();
                //char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                //decoder.GetChars(buffer, 0, bytes, chars, 0);

                //Console.WriteLine($"{DateTime.UtcNow.ToString("mm:ss")}: Got {bytes} bytes - {new string(chars)}");

                try
                {
                    string data = Encoding.ASCII.GetString(buffer);
                    FeedResponse response = JsonConvert.DeserializeObject<FeedResponse>(data); //new string(chars));

                    if (response == null) throw new Exception("Null message was recieved");
                    
                    foreach (var observer in observers)
                    {
                        observer.OnNext(response);
                    }
                }
                catch (Exception ex)
                {
                    foreach (var observer in observers)
                    {
                        observer.OnError(ex);
                    }
                }
            } while (bytes != 0 && !cancellationToken.IsCancellationRequested);

            if(bytes == 0)
            {
                //The server closed the connection
                hostClosedConnection = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            //Just in case an inherited class adds a finalizer
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            foreach (var observer in observers)
            {
                if (observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }
            observers.Clear();
        }

        public IDisposable Subscribe(IObserver<FeedResponse> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new FeedUnsubscriber(observers, observer);
        }

        private class FeedUnsubscriber : IDisposable
        {
            private readonly List<IObserver<FeedResponse>> allObservers;
            private readonly IObserver<FeedResponse> currentObserver;

            public FeedUnsubscriber(List<IObserver<FeedResponse>> allObservers, IObserver<FeedResponse> currentObserver)
            {
                this.allObservers = allObservers;
                this.currentObserver = currentObserver;
            }

            public void Dispose()
            {
                if (currentObserver != null && allObservers.Contains(currentObserver))
                {
                    allObservers.Remove(currentObserver);
                }
            }
        }
    }
}

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
    public abstract class NordnetFeed : IObservable<FeedResponseEnvelope>, IDisposable
    {
        /// <summary>
        /// When the SSL Socket has successfully connected
        /// </summary>
        public event EventHandler OnConnected;

        /// <summary>
        /// When the SSL Socket has disconnected
        /// </summary>
        public event EventHandler OnDisconnected;

        /// <summary>
        /// Change the default command waiting time before connecting
        /// </summary>
        public int CommandWaitTime { get; set; } = 100;

        private readonly List<IObserver<FeedResponseEnvelope>> observers = new List<IObserver<FeedResponseEnvelope>>();
        private readonly ConcurrentQueue<Command> commandQueue = new ConcurrentQueue<Command>();
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        private TcpClient tcpClient;
        private SslStream sslStream;

        private Thread txThread;
        private Thread rxThread;

        private bool hostClosedConnection = false;

        /// <summary>
        /// Connect to a Nordnet Feed
        /// </summary>
        /// <param name="hostname">Hostname of the feed</param>
        /// <param name="port">Port of the feed</param>
        /// <param name="sessionid">The sessionid recieved from the REST login</param>
        public void Connect(string hostname, int port, string sessionid)
        {
            ConnectToFeedAndStart(hostname, port, cts.Token);
            SendGenericCommand(new Command
            {
                cmd = "login",
                args = new Dictionary<string, string>
                {
                    {"session_key", sessionid}
                }
            });
        }

        /// <summary>
        /// Observe the stream of responses from the feed.
        /// </summary>
        /// <param name="observer">The observer that implements the IObserver interface</param>
        /// <returns>A disposable object that removes the observer from the feed when disposed</returns>
        public IDisposable Subscribe(IObserver<FeedResponseEnvelope> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new FeedUnsubscriber(observers, observer);
        }

        /// <summary>
        /// Dispose the feed and close all sockets
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //Just in case an inherited class adds a finalizer
            GC.SuppressFinalize(this);
        }

        protected void SendGenericCommand(Command cmd)
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
                try
                {
                    string data = Encoding.ASCII.GetString(buffer);
                    FeedResponse response = JsonConvert.DeserializeObject<FeedResponse>(data);

                    if (response == null) throw new Exception("Null message was recieved");

                    FeedResponseEnvelope envelope = CreateEnvelopeForResponse(response);
                    foreach (var observer in observers)
                    {
                        observer.OnNext(envelope);
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

        private FeedResponseEnvelope CreateEnvelopeForResponse(FeedResponse resp)
        {
            return null;
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
            cts.Cancel();
        }


        private class FeedUnsubscriber : IDisposable
        {
            private readonly List<IObserver<FeedResponseEnvelope>> allObservers;
            private readonly IObserver<FeedResponseEnvelope> currentObserver;

            public FeedUnsubscriber(List<IObserver<FeedResponseEnvelope>> allObservers, IObserver<FeedResponseEnvelope> currentObserver)
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

        private class FeedResponse
        {
            //"price", "depth", "trade", "indicator", "news", "trading_status", "heartbeat"
            public string Type { get; set; }
            public Dictionary<string, string> Data { get; set; }
        }
    }
}

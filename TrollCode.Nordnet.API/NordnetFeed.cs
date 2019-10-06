using System;
using System.Collections.Generic;
using System.Text;
using TrollCode.Nordnet.API.Responses;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using TrollCode.Nordnet.API.FeedModels;
using Newtonsoft.Json;

namespace TrollCode.Nordnet.API
{
    public abstract class NordnetFeed : IObservable<FeedResponse>, IDisposable
    {
        private readonly List<IObserver<FeedResponse>> observers;

        public NordnetFeed()
        {
            observers = new List<IObserver<FeedResponse>>();
        }

        public void ConnectToFeed(FeedInformation information, string sessionid)
        {
            using (TcpClient client = new TcpClient(information.Hostname, (int)information.Port))
            {
                Console.WriteLine("Client connected.");

                //NOTE to self: Does leaveInnerStreamOpen means that the inner TcpClient gets closed when the SslStream closes?
                using (SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                {
                    //Dont validate for now. Add later!
                    return true;

                }), null))
                {
                    try
                    {
                        sslStream.AuthenticateAsClient("I Will Fail Now");
                    }
                    catch (AuthenticationException e)
                    {
                        Console.WriteLine("Exception: {0}", e.Message);
                        if (e.InnerException != null)
                        {
                            Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                        }
                        Console.WriteLine("Authentication failed - closing the connection.");
                        client.Close();
                        return;
                    }

                    sslStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new Command
                    {
                        cmd = "login",
                        args = new Dictionary<string, string>
                        {
                            {"session_key", sessionid}
                        }
                    }) + "\n"));
                    sslStream.Flush();

                    sslStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new Command
                    {
                        //{"market_id":14,"country":"DK","name":"Nasdaq OMX Copenhagen"},
                        cmd = "subscribe", //"args":{"t":"price", "i":"1869", "m":30}}
                        args = new Dictionary<string, string>
                        {
                            {"t", "price"},
                            {"i", "1869" },
                            {"m", "14"}
                        }
                    }) + "\n"));
                    sslStream.Flush();

                    byte[] buffer = new byte[2048];
                    StringBuilder messageData = new StringBuilder();
                    int bytes = -1;
                    do
                    {
                        bytes = sslStream.Read(buffer, 0, buffer.Length);

                        Decoder decoder = Encoding.ASCII.GetDecoder();
                        char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                        decoder.GetChars(buffer, 0, bytes, chars, 0);

                        Console.WriteLine($"{DateTime.UtcNow.ToString("mm:ss")}: Got {bytes} bytes - {new string(chars)}");

                        try
                        {
                            FeedResponse response = JsonConvert.DeserializeObject<FeedResponse>(new string(chars));


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Could not parse feed response");
                            Console.WriteLine(ex.Message);
                        }
                    } while (bytes != 0);

                    Console.WriteLine($"The server has disconnected - Stopping");
                }
            }
        }

        private void Notify(FeedResponse message)
        {
            foreach (var observer in observers)
            {
                if(message == null)
                {
                    //TODO: Create own exception?
                    observer.OnError(new Exception("Null message was recieved"));
                }
                else
                {
                    observer.OnNext(message);
                } 
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

using System;
using System.Collections.Generic;
using System.Text;
using TrollCode.Nordnet.API.Responses;

namespace TrollCode.Nordnet.API
{
    public class NordnetFeed : IDisposable
    {
        private readonly LoginResponse session;
        public NordnetFeed(LoginResponse session)
        {
            this.session = session;
        }

        public void Dispose()
        {
            
        }
    }
}

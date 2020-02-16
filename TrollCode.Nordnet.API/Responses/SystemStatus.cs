using System;
using System.Collections.Generic;
using System.Text;

namespace Trollcode.Nordnet.API.Responses
{

//    Status {
//timestamp(integer) : Server timestamp.UNIX timestamp in milliseconds,
//valid_version (boolean): True if the API version is a valid version,
//system_running (boolean): Indicates if the system is running or temporarily stopped,
//message (string): Additional info. Usually empty
//}
    public class SystemStatus
    {
        public long Timestamp { get; set; }
        public bool Valid_version { get; set; }
        public bool System_running { get; set; }
        public string Message { get; set; }
    }
}

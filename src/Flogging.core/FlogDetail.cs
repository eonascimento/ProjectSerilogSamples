using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flogging.core
{
    public class FlogDetail
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        //Where
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }

        //Who

        public string UserId { get; set; }
        public string UserName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        //Everything else

        public long? ElapsedMiliseconds { get; set; } // only for performace entries
        public Exception Exception { get; set; } // The exception for error logging
        public string CorrelationId { get; set; } // exception shielding from server to client
        public Dictionary<string, object> AdditionalInfo { get; set; } // catch-all for anything.

        public FlogDetail()
        {
            Timestamp = DateTime.Now;
        }
    }
}

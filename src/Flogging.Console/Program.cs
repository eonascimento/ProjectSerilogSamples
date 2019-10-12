using Flogging.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flogging.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var fd = GetFlogDetail("starting applicaction", null);
            Flogger.WriteDiagnostic(fd);

            var tracker = new PerfTracker("FloggingConsoloe_execution", fd.UserId, fd.UserName, fd.Location, fd.Product, fd.Layer);

            try
            {
                var ex = new Exception("Something bad has happened");
                ex.Data.Add("input param", "nothing to see here");
                throw ex;
            }
            catch (Exception ex)
            {
                fd = GetFlogDetail("", ex);
                Flogger.WriteError(fd);
            }

            fd = GetFlogDetail("use flogging detail", null);
            Flogger.WriteUsage(fd);
            
            fd = GetFlogDetail("stopping app", null);
            Flogger.WriteDiagnostic(fd);

            tracker.Stop();
        }

        private static FlogDetail GetFlogDetail(string message, Exception ex)
        {
            return new FlogDetail
            {
                Product = "Flogger",
                Location = "FloggerConsole", //this application
                Layer = "Job", //unattended executable invoked somehow
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = message,
                Exception = ex
            };
        }
    }
}

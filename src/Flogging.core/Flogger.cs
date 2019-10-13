using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flogging.Core
{
    public static class Flogger
    {
        private static readonly ILogger _perfLogger;
        private static readonly ILogger _usageLogger;
        private static readonly ILogger _errorLogger;
        private static readonly ILogger _diagnosticLogger;

        static Flogger()
        {
            var pathprefix = "C:\\Projetos\\Estudo\\ProjectSerilogSamples\\Logs\\";

            _perfLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}perf.txt")
                .CreateLogger();

            _usageLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}usage.txt")
                .CreateLogger();

            _errorLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}error.txt")
                .CreateLogger();

            _diagnosticLogger = new LoggerConfiguration()
                .WriteTo.File(path: $"{pathprefix}diagnostic.txt")
                .CreateLogger();
        }

        public static void WritePerf(FlogDetail infoToLog)
        {
            _perfLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }
        public static void WriteUsage(FlogDetail infoToLog)
        {
            _usageLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }
        public static void WriteError(FlogDetail infoToLog)
        {
            _errorLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }
        public static void WriteDiagnostic(FlogDetail infoToLog)
        {
            var writeDiagnostic = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableDiagnostics"]);
            if (!writeDiagnostic)
                return;

            _diagnosticLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }


    }
}

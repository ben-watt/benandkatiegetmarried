using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace benandkatiegetmarried.Common.Logging
{
    public class Logger : ILogger
    {
        public void Error(string message)
        {
            Trace.TraceError(message);
        }

        public void Information(string message)
        {
            Trace.TraceInformation(message);
        }

        public void Information(string format, params object[] args )
        {
            Trace.TraceInformation(format, args);
        }

        public void Warning(string message)
        {
            Trace.TraceWarning(message);
        }
    }
}

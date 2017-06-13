using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Logging
{
    public interface ILogger
    {
        void Error(string message);
        void Warning(string message);
        void Information(string message);
        void Information(string format, params object[] args);
    }
}

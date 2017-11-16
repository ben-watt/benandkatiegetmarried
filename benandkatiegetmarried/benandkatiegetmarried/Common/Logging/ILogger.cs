using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Logging
{
    public interface ILogger
    {
        void SetSessionId(string userId);
        void Error(Exception error);
        void Information(string message);
        void Information(string message, IDictionary<string, string> data);
        void Information(string format, params object[] args);
        void Warning(string message);
        void Metric(string name, double value, IDictionary<string, string> propertys = null);
        void Request(IDictionary<string,string> properties );
    }
}

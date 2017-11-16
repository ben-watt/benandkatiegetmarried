using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace benandkatiegetmarried.Common.Logging
{
    public class Logger : ILogger
    {
        private TelemetryClient _telemetry = new TelemetryClient();
        public Logger()
        {
            _telemetry.Context.InstrumentationKey = "86df9502-de1b-4469-b636-1783ffdb94cd";
        }

        public void SetSessionId(string userId)
        {
            _telemetry.Context.User.Id = userId ?? Guid.Empty.ToString();
        }

        public void Error(Exception error)
        {
            _telemetry.TrackException(error);
        }
        public void Information(string message)
        {
            _telemetry.TrackEvent(message);
        }

        public void Information(string message, IDictionary<string,string> data)
        {
            _telemetry.TrackEvent(message, data);
        }

        public void Information(string format, params object[] args )
        {
            _telemetry.TrackEvent(String.Format(format, args));
        }

        public void Warning(string message)
        {
            _telemetry.TrackEvent("Warning", new Dictionary<string,string>() { { "Message", message } });
        }

        public void Metric(string name, double value, IDictionary<string, string> propertys = null)
        {
            _telemetry.TrackMetric(name, value, propertys);
        }
        public void Request(IDictionary<string,string> properties)
        {
            var request = new RequestTelemetry();

            foreach (var item in properties)
                request.Properties.Add(new KeyValuePair<string, string>(item.Key, item.Value));
            _telemetry.TrackRequest(request);
        }
    }
}

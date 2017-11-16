using Nancy.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using benandkatiegetmarried.Common.Logging;
using Microsoft.ApplicationInsights;
using benandkatiegetmarried.Models;

namespace benandkatiegetmarried
{
    public class Logging : IRequestStartup
    {
        private ILogger _log;

        public Logging()
        {
            _log = new Logger();
        }
        public void Initialize(IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline((ctx) =>
            {
                var c = (NancyContext)ctx;
                c.Items["stopwatch"] = DateTime.UtcNow;
                var invite = (Invite)ctx?.CurrentUser;
                _log.SetSessionId(invite?.Id.ToString());
                return null;
            });

            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                var startTime = (DateTime)ctx.Items["stopwatch"];
                var requestTime = (DateTime.UtcNow - startTime);

                _log.Request(new Dictionary<string, string>());
            });

            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                _log.Error(ex);
                return null;
            });
        }
    }
}

using Nancy.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using benandkatiegetmarried.Common.Logging;

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
                _log.Information("Request Starting");
                return null;
            });

            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                _log.Information("Ending Request");

                var requestTime = (DateTime.UtcNow - (DateTime)ctx.Items["stopwatch"]).TotalSeconds;
                _log.Information("Request took {0}s", requestTime);
            });

            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                _log.Error(ex.TargetSite.MethodHandle.ToString());
                _log.Error(ex.TargetSite.Module.FullyQualifiedName);
                return null;
            });
        }
    }
}

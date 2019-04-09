using benandkatiegetmarried.Common.Logging;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.ErrorHandling
{
    public static class ErrorHandling
    {
        public static void Enable(IPipelines pipelines, ILogger logger)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, e) =>
            {
                logger.Information("Error", new Dictionary<string, string>() {
                    { "InviteId", ctx.CurrentUser.UserName },
                    { "Request", JsonConvert.SerializeObject(ctx.Request) },
                    { "Error",  e.Message },
                    { "StackTrace", e.StackTrace }
                });

                return ErrorResponse.FromException(e);
            });
        }
    }
}

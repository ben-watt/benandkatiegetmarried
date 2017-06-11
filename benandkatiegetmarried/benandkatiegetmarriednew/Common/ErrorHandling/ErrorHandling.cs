using Nancy.Bootstrapper;
using Nancy.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.ErrorHandling
{
    public static class ErrorHandling
    {
        public static void Enable(IPipelines pipelines)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, e) =>
            {
                return ErrorResponse.FromException(e);
            });
        }
    }
}

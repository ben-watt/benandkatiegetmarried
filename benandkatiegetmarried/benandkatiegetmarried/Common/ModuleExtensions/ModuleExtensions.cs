using Nancy;
using Nancy.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.ModuleExtensions
{
    public static class ModuleExtensions
    {
        public static IEnumerable<T> GetFromSession<T>(this NancyModule module, string sessionKey) where T : struct
        {
            try
            {
                var eventId = (IEnumerable<T>)module.Request.Session[sessionKey];
                if(eventId != null)
                {
                    return eventId;
                }
                return new List<T>();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }
    }

    public static class SessionExtensions
    {
    }
}

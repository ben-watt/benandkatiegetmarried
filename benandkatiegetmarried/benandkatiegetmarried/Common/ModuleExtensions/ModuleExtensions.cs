using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.ModuleExtensions
{
    public static class ModuleExtensions
    {
        public static Guid? GetIdFromSession(this NancyModule module, string sessionKey)
        {
            try
            {
                var eventId = (Guid)module.Request.Session[sessionKey];
                return eventId;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

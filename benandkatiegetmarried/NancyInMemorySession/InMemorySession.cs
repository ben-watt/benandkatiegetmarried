using Nancy;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.TinyIoc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyInMemorySession
{
    public class InMemorySessions
    {
        private InMemorySessions() { }

        public static void Enable(IPipelines pipelines, TinyIoCContainer container)
        {
           pipelines.BeforeRequest.AddItemToEndOfPipeline(ctx =>
           {
               ISession session = null;
               if (ctx.CurrentUser != null)
               {
                   session = SessionStore.GetSessionByPartitionKey(ctx.CurrentUser.UserName);
               }

               session = session ?? new Session();
               ctx.Request.Session = session;
               
               container.Register(typeof(ISession), session);
               return null;
           });
            
           pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
           {
               var user = ctx.CurrentUser;
               if(user != null)
               {
                   var partitionKey = ctx.CurrentUser.UserName;
                   var session = ctx.Request.Session;

                   if (session != null)
                   {
                       SessionStore.Add(partitionKey, session);
                   }
               }
           });
        }
    }

    public static class SessionStore
    {
        public static Dictionary<string, ISession> sessions = new Dictionary<string, ISession>();

        public static ISession GetSessionByPartitionKey(string partitionKey)
        {
            ISession session;
            if (sessions.TryGetValue(partitionKey, out session))
            {
                return session;
            };
            return null;
        }
        public static void Add(string partitionKey, ISession session) {
            sessions[partitionKey] = session;
        }
    }
}

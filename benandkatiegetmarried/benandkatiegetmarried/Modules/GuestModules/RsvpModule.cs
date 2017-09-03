using benandkatiegetmarried.UseCases;
using benandkatiegetmarried.UseCases.Rsvp;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules.GuestModules
{
    public class RsvpModule : NancyModule
    {
        private IHandler<RsvpRequest, RsvpResponse> _rsvpHandler;
        public RsvpModule(IHandler<RsvpRequest, RsvpResponse> rsvpHandler) 
            : base("api/guest/{eventId}")
        {
            this.RequiresAuthentication();
            this.RequiresClaims("Guest");

            _rsvpHandler = rsvpHandler;

            Post["/"] = _ => CreateRsvp();
        }
        private dynamic CreateRsvp()
        {
            var request = this.Bind<RsvpRequest>();
            if(request != null)
            {
                _rsvpHandler.Handle(request);
            }
            return HttpStatusCode.BadRequest;
        }
    }
}

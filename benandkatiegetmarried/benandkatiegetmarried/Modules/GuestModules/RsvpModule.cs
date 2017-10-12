using benandkatiegetmarried.Models;
using benandkatiegetmarried.UseCases;
using benandkatiegetmarried.UseCases.Rsvp;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace benandkatiegetmarried.Modules.GuestModules
{
    public class RsvpModule : NancyModule
    {
        private IHandler<RsvpRequest, UseCases.Rsvp.RsvpResponse> _rsvpHandler;
        public RsvpModule(IHandler<RsvpRequest, UseCases.Rsvp.RsvpResponse> rsvpHandler) 
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
            var invite = (Invite)this.Context.CurrentUser;
            Guid inviteId = invite.Id;
            if (request != null && inviteId != null)
            {
                request.Rsvp.InviteId = inviteId;
                request.Rsvp.LinkResponses();

                var rsvpResposne = _rsvpHandler.Handle(request);
                if (rsvpResposne.IsValid)
                {
                    return Response.AsJson(rsvpResposne, HttpStatusCode.Created);
                }
            }
            return HttpStatusCode.BadRequest;
        }
    }
}

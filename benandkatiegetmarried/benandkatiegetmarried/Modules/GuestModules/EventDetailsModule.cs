using benandkatiegetmarried.Common.ModuleExtensions;
using benandkatiegetmarried.DAL.GuestEventDetails.Queries;
using Nancy;
using Nancy.Security;
using Nancy.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules.GuestModules
{
    public class EventDetailsModule : NancyModule
    {
        private IGuestEventDetailsQueries<Guid> _queries;

        public EventDetailsModule(IGuestEventDetailsQueries<Guid> queries)
            : base("api/{eventId}")
        {

            this.RequiresAuthentication();
            this.RequiresClaims("Guest");

            _queries = queries;

            Get["/guests-on-invite"] = _ => GetGuestsOnInvite();
            Get["/event-details"] = p => GetEventDetails(p.eventId);
            Get["/featured-guests"] = p => GetFeaturedGuests(p.eventId);
            Get["/venue-details"] = p => GetVenueDetails(p.eventId);
        }

        private dynamic GetGuestsOnInvite()
        {
            var inviteId = Request.Headers["inviteId"].First();
            if (String.IsNullOrEmpty(inviteId)) return new ArgumentException("Must provide an inviteId");   
            
            return _queries.GetGuestsOnInvite(Guid.Parse(inviteId));
        }

        private dynamic GetEventDetails(Guid eventId)
        {
            return _queries.GetEventDetails(eventId);
        }
        private dynamic GetFeaturedGuests(Guid eventId)
        {
            return _queries.GetFeaturedGuests(eventId);
        }
        private dynamic GetVenueDetails(Guid eventId)
        {
            return _queries.GetVenueDetails(eventId);
        }
    }
}

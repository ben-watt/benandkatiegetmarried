using benandkatiegetmarried.Common.Security;
using benandkatiegetmarried.DAL.GuestEventDetails.Queries;
using Nancy;
using Nancy.Security;
using System;

namespace benandkatiegetmarried.Modules.GuestModules
{
    public class EventDetailsModule : NancyModule
    {
        private IGuestEventDetailsQueries<Guid> _queries;
        private IIdentity _invite;

        public EventDetailsModule(IGuestEventDetailsQueries<Guid> queries)
            : base("api/{eventId}")
        {
            this.Before.AddItemToEndOfPipeline(ctx =>
            {
                _invite = (IIdentity)ctx.CurrentUser;
                return null;
            });

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
            return _queries.GetGuestsOnInvite(this._invite.Id);
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

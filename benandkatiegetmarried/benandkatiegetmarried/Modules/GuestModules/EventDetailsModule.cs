using benandkatiegetmarried.Common.ModuleExtensions;
using benandkatiegetmarried.DAL.GuestEventDetails.Queries;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules.GuestModules
{
    public class EventDetailsModule : NancyModule
    {
        private IGuestEventDetailsQueries _queries;

        public EventDetailsModule(IGuestEventDetailsQueries queries) : base("api")
        {
            _queries = queries;

            Get["/venue-details"] = _ => GetVenueDetails();
            Get["/featured-guests"] = _ => GetFeaturedGuests();
            Get["/event-details"] = _ => GetEventDetails();
            Get["/guests-on-invite"] = _ => GetGuestsOnInvite();
        }

        private dynamic GetGuestsOnInvite()
        {
            return RunQuery((x) => _queries.GetEventDetails(x), "guest-inviteId");
        }

        private dynamic GetEventDetails()
        {
            return RunQuery((x) => _queries.GetEventDetails(x), "guest-eventId");
        }
        private dynamic GetFeaturedGuests()
        {
            return RunQuery((x) => _queries.GetFeaturedGuests(x), "guest-eventId");
        }
        private dynamic GetVenueDetails()
        {
            return RunQuery((x) => _queries.GetVenueDetails(x), "guest-eventId");
        }

        private dynamic RunQuery(Func<Guid, dynamic> query, string sessionKey)
        {
            Guid? eventId = this.GetIdFromSession(sessionKey);
            if (eventId != null)
            {
                return query.Invoke((Guid)eventId);
            }
            return HttpStatusCode.BadRequest;
        }
    }
}

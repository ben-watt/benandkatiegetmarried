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
        private ISession _session;

        public EventDetailsModule(IGuestEventDetailsQueries<Guid> queries
            , ISession session): base("api")
        {

            this.RequiresAuthentication();
            this.RequiresClaims("Guest");

            _queries = queries;
            _session = session;

            Get["/venue-details"] = _ => GetVenueDetails();
            Get["/featured-guests"] = _ => GetFeaturedGuests();
            Get["/event-details"] = _ => GetEventDetails();
            Get["/guests-on-invite"] = _ => GetGuestsOnInvite();
        }

        private dynamic GetGuestsOnInvite()
        {
            return RunQuery((x) => _queries.GetGuestsOnInvite(x), "guest-inviteId");
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
            var eventId = (IEnumerable<Guid>)_session[sessionKey];
            if (eventId.Count() > 0)
            {
                return query.Invoke(eventId.FirstOrDefault());
            }
            throw new ArgumentNullException($"Could not find {sessionKey} from session");
        }
    }
}

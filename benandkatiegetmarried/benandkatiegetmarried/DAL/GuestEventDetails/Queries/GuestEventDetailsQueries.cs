using benandkatiegetmarried.Models;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.GuestEventDetails.Queries
{
    public class GuestEventDetailsQueries<Guid> : IGuestEventDetailsQueries<Guid>
    {
        private IWeddingDatabase _db;
        public GuestEventDetailsQueries(IWeddingDatabase db)
        {
            _db = db;
        }
        public Models.Event GetEventDetails(Guid eventId)
        {
            Models.Event eventDetails;
            using(var uow = _db.GetTransaction())
            {
                eventDetails = _db.FirstOrDefault<Models.Event>("WHERE Id = @0", eventId);
                uow.Complete();
            }
            return eventDetails;
        }

        public IEnumerable<Models.Guest> GetFeaturedGuests(Guid eventId)
        {
            IEnumerable<Models.Guest> guests;
            using (var uow = _db.GetTransaction())
            {
                guests = _db.Query<Models.Guest>("WHERE EventId = @0 AND IsFeatured = 1", eventId);
                uow.Complete();
            }
            return guests;
        }

        public IEnumerable<Models.Guest> GetGuestsOnInvite(Guid inviteId)
        {
            IEnumerable<Models.Guest> guests;
            using (var uow = _db.GetTransaction())
            {
                guests = _db.Query<Models.Guest>("WHERE InviteId = @0", inviteId);
                uow.Complete();
            }
            return guests;
        }

        public IEnumerable<Models.Venue> GetVenueDetails(Guid eventId)
        {
            IEnumerable<Models.Venue> venues;
            using (var uow = _db.GetTransaction())
            {
                venues = _db.Query<Models.Venue>("WHERE EventId = @0", eventId);
                uow.Complete();
            }
            return venues;
        }
    }
}

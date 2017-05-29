using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Rsvp.RsvpQueries
{
    public class RsvpQueries : IRsvpQueries
    {
        private IDatabase _db;

        public RsvpQueries(IDatabase db)
        {
            _db = db;
        }
        public IEnumerable<RSVP> GetAll()
        {
            IEnumerable<RSVP> result;
            using(var uow = _db.GetTransaction())
            {
                result = _db.Query<RSVP>("");
                uow.Complete();
            }
            return result;
        }

        public IEnumerable<RSVP> GetByGuestIds(IEnumerable<Guid> guestIds)
        {
            IEnumerable<RSVP> result;
            using (var uow = _db.GetTransaction())
            {
                result = _db.Query<RSVP>("WHERE GuestId IN (@0)", guestIds);
                uow.Complete();
            }
            return result;
        }
    }
}

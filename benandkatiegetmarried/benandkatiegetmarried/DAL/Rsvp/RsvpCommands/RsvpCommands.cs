using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Rsvp.RsvpCommands
{
    public class RsvpCommands : IRsvpCommands
    {
        private IDatabase _db;
        public RsvpCommands(IDatabase db)
        {
            _db = db;
        }
        public void Create(IEnumerable<RSVP> rsvps)
        {
            using(var uow = _db.GetTransaction())
            {
                foreach (var rsvp in rsvps)
                {
                    _db.Insert(rsvp);
                    _db.Update<Models.Guest>(@"UPDATE core.Guests 
                                            SET HasSentRsvp = 1 
                                            WHERE Id = @0", rsvp.GuestId);
                }
                uow.Complete();
            }
        }
    }
}

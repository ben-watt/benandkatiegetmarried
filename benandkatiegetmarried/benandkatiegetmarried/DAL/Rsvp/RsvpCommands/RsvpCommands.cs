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
        private IWeddingDatabase _db;
        public RsvpCommands(IWeddingDatabase db)
        {
            _db = db;
        }

        public void Create(Models.Rsvp rsvp)
        {
            using (var uow = _db.GetTransaction())
            {
                    _db.Insert(rsvp);
                    _db.Insert(rsvp.Responses);
                    _db.Update<Models.Guest>(@"UPDATE core.Guests 
                                            SET HasSentRsvp = 1 
                                            WHERE Id IN (@0)",
                                            rsvp.Responses.Select(x => x.GuestId));
                uow.Complete();
            }
        }
    }
}

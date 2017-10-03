using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Venue.VenueCommands
{
    public class VenueCommands : CrudCommands<Models.Venue, Guid>, IVenueCommands
    {
        public VenueCommands(IWeddingDatabase db) : base(db)
        {
        }
    }
}

using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Venue.VenueCommands
{
    public class VenueCommands : BaseCommands<Models.Venue>, IVenueCommands
    {
        public VenueCommands(IDatabase db) : base(db)
        {
        }
    }
}

using benandkatiegetmarried.DAL.BaseQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Venue.VenueQueries
{
    class VenueQueries : BaseQueries<Models.Venue, Guid>, IVenueQueries
    {
        public VenueQueries(IDatabase db) : base(db)
        {
        }
    }
}

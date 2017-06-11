using benandkatiegetmarried.DAL.BaseQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.Venue.VenueQueries
{
    public interface IVenueQueries : IEventCrudQueries<Models.Venue, Guid>
    {
    }
}

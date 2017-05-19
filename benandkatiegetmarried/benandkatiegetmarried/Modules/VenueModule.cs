using benandkatiegetmarried.DAL.Venue.VenueCommands;
using benandkatiegetmarried.DAL.Venue.VenueQueries;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public class VenueModule : NancyModule
    {
        IVenueQueries _queries;
        IVenueCommands _commands;
        public VenueModule(IVenueQueries queries
            , IVenueCommands commands) : base("venues")
        {
            _queries = queries;
            _commands = commands;

            Get["/"] = _ => GetAll();
            Get["/{id}"] = p => GetById(p.id);
        }

        private dynamic GetById(dynamic id)
        {
            Guid venueId;
            if(Guid.TryParse(id, out venueId))
            {
                return _queries.GetById(venueId);
            }
            return HttpStatusCode.BadRequest;
        }

        private dynamic GetAll()
        {
            return _queries.GetAll();
        }
    }
}

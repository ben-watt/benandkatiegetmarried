using benandkatiegetmarried.DAL.Venue.VenueCommands;
using benandkatiegetmarried.DAL.Venue.VenueQueries;
using benandkatiegetmarried.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public class VenueModule : CrudModule<Venue, Guid>
    {
        IVenueQueries _queries;
        IVenueCommands _commands;
        public VenueModule(IVenueQueries q, IVenueCommands c) 
            : base("venues", q , c )
        {
            _queries = q;
            _commands = c;
        }
    }
}

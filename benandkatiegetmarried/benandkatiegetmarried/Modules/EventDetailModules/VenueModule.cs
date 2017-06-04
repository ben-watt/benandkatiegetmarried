using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.Venue.VenueCommands;
using benandkatiegetmarried.DAL.Venue.VenueQueries;
using benandkatiegetmarried.Models;
using FluentValidation;
using Nancy;
using Nancy.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public class VenueModule : EventDetailsBaseModule<Venue, Guid>
    {
        IVenueQueries _queries;
        IVenueCommands _commands;
        public VenueModule(IVenueQueries queries
            , IVenueCommands commands
            , IValidator<Venue> validator
            , ISession session) : base("venues", queries , commands , validator, session)
        {
            _queries = queries;
            _commands = commands;
        }
    }
}

using benandkatiegetmarried.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Security;
using System;
using Nancy.Authentication.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.BaseQueries;
using benandkatiegetmarried.DAL.Guest.Commands;

namespace benandkatiegetmarried.Modules
{
    public class GuestModule : EventDetailsBaseModule<Guest, Guid>
    {
        public GuestModule(IGuestQueries queries
            , IGuestCommands commands
            , IValidator<Guest> validator) : base("guests", queries, commands, validator) {}
    }
}

using benandkatiegetmarried.Models;
using benandkatiegetmarried.UseCases;
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

namespace benandkatiegetmarried.Modules
{
    public class GuestModule : NancyModule
    {
        public GuestModule() : base("api/guests")
        {
            this.RequiresAuthentication();
            Get["/"] = _ => GetGuestsRequest();
        }

        private dynamic GetGuestsRequest()
        {
            return new Guest { Id = Guid.NewGuid(), FirstName = "Ben", LastName = "Katie" };
        }
    }
}

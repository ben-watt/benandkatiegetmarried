using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using benandkatiegetmarried.Modules;
using Moq;
using benandkatiegetmarried.DAL.Venue.VenueCommands;
using benandkatiegetmarried.DAL.Venue.VenueQueries;
using benandkatiegetmarried.Models;
using Nancy.Testing;
using Nancy;
using benandkatiegetmarried.DAL.BaseQueries;
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.Common.Validation;
using Nancy.Authentication.Forms;
using benandkatiegetmarried.DAL.Login;

namespace benandkatiegetmarriedTests.Modules
{
    public class VenueModuleTests 
        : BaseModuleTests<VenueModule
            , IVenueQueries
            , IVenueCommands
            , IValidator<Venue>
            , Venue>
    {
        private Guid _eventId = Guid.NewGuid();

        public VenueModuleTests() { }

        [Fact]
        public void GetAllReturnsAllVenues()
        {
            _queries.Setup(x => x.GetAll()).Returns(() => new List<Venue> { new Venue { Id = _eventId, EventId = Guid.Empty, Postcode = "M21 7JS", Name = "Home" } });

            var bootstrapper = BootstrapBuilder()
                .WithLoggedInUser("Ben")
                .Build();

            var response = GetApiBrowser(bootstrapper).Get($"api/events/{_eventId}/venues");
        

            var model = response.Body.DeserializeJson<IEnumerable<Venue>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, model.Count());
            Assert.Equal("application/json; charset=utf-8", response.Body.ContentType);
            Assert.Equal("M21 7JS", model.FirstOrDefault().Postcode);

        }

        [Fact]
        public void GetById_ShouldReturnTheCorrectVenue()
        {
            var bootstrapper = BootstrapBuilder()
                .WithLoggedInUser("Katie")
                .Build();

            _queries.Setup(x => x.GetById(Guid.Empty)).Returns(new Venue() { Id = Guid.Empty, Postcode = "M21 7JS" });

            var response = GetApiBrowser(bootstrapper).Get(
                $"api/events/{_eventId}/venues/{Guid.Empty.ToString()}"
            );

            var model = response.Body.DeserializeJson<Venue>();

            Assert.Equal("M21 7JS", model.Postcode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Body.ContentType);
            Assert.Equal(Guid.Empty, model.Id);
        }
    }
}

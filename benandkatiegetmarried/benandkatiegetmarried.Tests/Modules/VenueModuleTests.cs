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

namespace benandkatiegetmarriedTests.Modules
{
    public class VenueModuleTests
    {
        private Mock<IVenueQueries> _queries;
        private Mock<IVenueCommands> _commands;
        private ConfigurableBootstrapper _bootstrapper;

        public VenueModuleTests()
        {
            _queries = new Mock<IVenueQueries>();
            _commands = new Mock<IVenueCommands>();
            _bootstrapper = new ConfigurableBootstrapper(config =>
                    config.Module<VenueModule>()
                        .Dependency(_queries.Object)
                        .Dependency(_commands.Object)
                );
        }
        [Fact]
        public void GetAllReturnsAllVenues()
        {
            _queries.Setup(x => x.GetAll()).Returns(() => new List<Venue> { new Venue { VenueId = Guid.Empty, PostCode = "M21 7JS", Name = "Home" } });

            var browser = new Browser(_bootstrapper, x =>
                x.Header("Accept", "application/json"));

            var response = browser.Get("/venues");
            var model = response.Body.DeserializeJson<IEnumerable<Venue>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, model.Count());
            Assert.Equal("application/json; charset=utf-8", response.Body.ContentType);
            Assert.Equal("M21 7JS", model.FirstOrDefault().PostCode);

        }

        [Fact]
        public void GetById_ShouldReturnTheCorrectVenue()
        {
            _queries.Setup(X => X.GetById(Guid.Empty)).Returns(() => new Venue { VenueId = Guid.Empty, PostCode = "M21 7JS" });

            var browser = new Browser(_bootstrapper, x =>
                x.Header("Accept", "application/json"));

            var response = browser.Get("/venues/" + Guid.Empty.ToString());
            var model = response.Body.DeserializeJson<Venue>();

            Assert.Equal("M21 7JS", model.PostCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Body.ContentType);
            Assert.Equal(Guid.Empty, model.VenueId);
        }
    }
}

using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.Guest.Commands;
using benandkatiegetmarried.Models;
using benandkatiegetmarried.Modules;
using Moq;
using Nancy;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Nancy.Security;
using FluentValidation;
using benandkatiegetmarried.DAL.Guest.Queries;

namespace benandkatiegetmarriedTests
{
    public class GuestModuleTests
    {
        private Mock<IGuestQueries> _queries;
        private Mock<IGuestCommands> _commands;
        private Mock<IValidator<Guest>> _validator;
        private ConfigurableBootstrapper _bootstrapper;
        private string baseRoot = "api/events/0";
        private string moduleRoot = "/guests";
        private string fullRoot;
        private Browser _apiBrowser;

        public GuestModuleTests()
        {
            _queries = new Mock<IGuestQueries>();
            _commands = new Mock<IGuestCommands>();
            _validator = new Mock<IValidator<Guest>>();
            _bootstrapper = new ConfigurableBootstrapper(config =>
            {
                config.Module<GuestModule>()
                    .Dependency(_queries.Object)
                    .Dependency(_commands.Object)
                    .Dependency(_validator.Object);
            });

            _apiBrowser = new Browser(_bootstrapper, x =>
                x.Header("Accept", "application/json"));

            fullRoot = baseRoot + moduleRoot;
        }

        [Fact]
        public void Sould_Return_unauthrised_if_users_is_not_valid()
        {
            var response = _apiBrowser.Get(fullRoot);
            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        public void Should_Return_List_Of_Guests_On_Invite_Code_Supplied()
        {
            var result = _apiBrowser.Get(fullRoot);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
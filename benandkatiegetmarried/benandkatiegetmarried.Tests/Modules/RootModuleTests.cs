using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Nancy.Testing;
using Nancy;
using benandkatiegetmarried.Modules;
using benandkatiegetmarried;
using System.IO;
using Nancy.ViewEngines;
using Moq;
using benandkatiegetmarried.UseCases.Login;
using benandkatiegetmarried.DAL.UserEvents;
using benandkatiegetmarried.UseCases;
using FluentValidation;
using FluentValidation.Results;
using Nancy.Session;

namespace benandkatiegetmarriedTests
{
    public class RootModuleTests
    {
        private Mock<IHandler<GuestLoginRequest, GuestLoginResponse>> _loginHandler 
            = new Mock<IHandler<GuestLoginRequest, GuestLoginResponse>>();
        private Mock<IHandler<UserLoginRequest, UserLoginResponse>> _userLoginHandler
            = new Mock<IHandler<UserLoginRequest, UserLoginResponse>>();
        private Mock<IHandler<GuestLoginRequest, GuestLoginResponse>> _guestLoginHandler
            = new Mock<IHandler<GuestLoginRequest, GuestLoginResponse>>();
        private Mock<IUserQueries> _userEventQueries = new Mock<IUserQueries>();
        private Mock<IValidator<GuestLoginRequest>> _guestValidator = new Mock<IValidator<GuestLoginRequest>>();
        private Mock<IValidator<UserLoginRequest>> _userValidator = new Mock<IValidator<UserLoginRequest>>();

        private Browser _browser;
        private ConfigurableBootstrapper _bootstrapper;

        public RootModuleTests()
        {
            _bootstrapper = new CustomTestBootstrapper(with =>
            {
                with.Module<RootModule>()
                .Dependency(_loginHandler.Object)
                .Dependency(_userEventQueries.Object)
                .Dependency(_guestLoginHandler.Object)
                .Dependency(_userLoginHandler.Object)
                .Dependency(_guestValidator.Object)
                .Dependency(_userValidator.Object);
            });
            _browser = new Browser(_bootstrapper);
        }

        [Fact]
        public void Ensure_RootPath_Hits_HomePage()
        {

            var response = _browser.Get("/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Test_Fail_Validation_Response()
        {
            this._userValidator.Setup(x => x.Validate(It.IsAny<object>())).Returns(
                new ValidationResult(new List<ValidationFailure> { new ValidationFailure("test", "rubbish") }));

            var response = _browser.Get("/user-login");

        }

    }
}

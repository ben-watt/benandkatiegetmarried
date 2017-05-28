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

namespace benandkatiegetmarriedTests
{
    public class RootModuleTests
    {
        private Mock<IHandler<GuestLoginRequest, GuestLoginResponse>> _loginHandler 
            = new Mock<IHandler<GuestLoginRequest, GuestLoginResponse>>();
        private Mock<IHandler<UserLoginRequest, UserLoginResponse>> _userLoginHandler
            = new Mock<IHandler<UserLoginRequest, UserLoginResponse>>();
        private Mock<IUserQueries> _userEventQueries = new Mock<IUserQueries>();

        [Fact]
        public void Ensure_RootPath_Hits_HomePage()
        {
            var bootstrapper = new CustomTestBootstrapper(with =>
            {
                with.Module<RootModule>()
                .Dependency(_loginHandler.Object)
                .Dependency(_userEventQueries.Object)
                .Dependency(_userLoginHandler.Object);
            });
            var browser = new Browser(bootstrapper);
            var response = browser.Get("/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Test_Root_Path()
        {
            IRootPathProvider root = new CustomRootPathProvider();
            var rootPath = root.GetRootPath();
            Assert.Equal(Path.GetFullPath("../.."), rootPath);
        }
    }
}

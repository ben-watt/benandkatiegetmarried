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

namespace benandkatiegetmarriedTests
{
    public class RootModuleTests
    {
        [Fact]
        public void Ensure_RootPath_Hits_HomePage()
        {
            var bootstrapper = new CustomTestBootstrapper(with =>
            {
                with.Module<RootModule>();
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

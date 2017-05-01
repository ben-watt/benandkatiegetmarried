using benandkatiegetmarried.Modules;
using Nancy;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace benandkatiegetmarriedTests
{
    public class GuestModuleTests
    {
        [Fact]
        public void Sould_Return_unauthrised_if_users_is_not_valid()
        {
            var bootstrapper = new CustomTestBootstrapper(with =>
            {
                with.Module<GuestModule>();
            });
            var browser = new Browser(bootstrapper, x => x.Header("ContentType", "application/json"));

            var response = browser.Get("/api/guests");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        public void Should_Return_List_Of_Guests_On_Invite_Code_Supplied()
        {
            var bootstrapper = new CustomTestBootstrapper(with =>
            {
                with.Module<GuestModule>();
            });
            var browser = new Browser(bootstrapper);

            var result = browser.Get("/api/guests");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
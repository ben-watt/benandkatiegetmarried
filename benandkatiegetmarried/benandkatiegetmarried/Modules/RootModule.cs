using benandkatiegetmarried.UseCases.Login;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using Nancy.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules
{
    public class RootModule : NancyModule
    {
        private IHandler<GuestLoginRequest, LoginResponse> _loginHandler;
        public RootModule(IHandler<GuestLoginRequest, LoginResponse> loginHandler)
        {
            _loginHandler = loginHandler;

            Get["/"] = _ => View["LandingPage"];
            Post["/userLogin"] = _ => UserLogin();
            Post["/guestLogin"] = _ => GuestLogin();
            Post["/logout"] = _ => Logout();
        }

        private dynamic Logout()
        {
            return this.LogoutAndRedirect("/");
        }

        private dynamic UserLogin()
        {
            var request = this.Bind<GuestLoginRequest>();
            return Login(request);
        }
        private dynamic GuestLogin()
        {
            var request = this.Bind<GuestLoginRequest>();
            return Login(request);
        }

        private dynamic Login(GuestLoginRequest request)
        {
            if (request != null)
            {
                var response = _loginHandler.Handle(request);
                if (response.IsValid)
                {
                    this.Login(response.Id, DateTime.Now.AddDays(7), "/");
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }
            return this.Response.AsRedirect("/", RedirectResponse.RedirectType.SeeOther)
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }
    }
}

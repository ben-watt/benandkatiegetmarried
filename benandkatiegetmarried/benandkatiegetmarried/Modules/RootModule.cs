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
        private IHandler<LoginRequest, LoginResponse> _loginHandler;
        public RootModule(IHandler<LoginRequest, LoginResponse> loginHandler)
        {
            _loginHandler = loginHandler;

            Get["/"] = _ => View["LandingPage"];
            Post["/login"] = _ => Login();
            Post["/logout"] = _ => Logout();
        }

        private dynamic Logout()
        {
            return this.LogoutAndRedirect("/");
        }

        private dynamic Login()
        {
            var request = this.Bind<LoginRequest>();
            if(request != null)
            {
                var response = _loginHandler.Handle(request);
                if (response.IsValid)
                {
                    this.Login(response.InviteId, DateTime.Now.AddDays(7), "/");
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }           
            return this.Response.AsRedirect("/", RedirectResponse.RedirectType.SeeOther)
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }
    }
}

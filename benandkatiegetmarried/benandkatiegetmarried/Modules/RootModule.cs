using benandkatiegetmarried.UseCases;
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
        private IRequestHandler<LoginRequest, LoginResponse> _loginHandler;
        public RootModule(IRequestHandler<LoginRequest, LoginResponse> loginHandler)
        {
            this._loginHandler = loginHandler;
            Get["/"] = _ => View["LandingPage"];
            Post["/login"] = _ => Login();
        }
        private dynamic Login()
        {
            var request = this.Bind<LoginRequest>();
            var response = _loginHandler.Handle(request);
            if (response.IsValid)
            {
                this.Login(response.InviteId, DateTime.Now.AddDays(7), "/");
            }
            return this.Response.AsRedirect("/", RedirectResponse.RedirectType.SeeOther)
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }
    }
}

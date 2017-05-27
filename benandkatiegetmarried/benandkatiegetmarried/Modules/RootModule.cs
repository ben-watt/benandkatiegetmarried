using benandkatiegetmarried.DAL.UserEvents;
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
        private IUserEventsQueries _userEventQueries;
        public RootModule(IHandler<GuestLoginRequest, LoginResponse> loginHandler
            , IUserEventsQueries userEventQueries)
        {
            _loginHandler = loginHandler;
            _userEventQueries = userEventQueries;

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
            if (request != null)
            {
                var response = _loginHandler.Handle(request);
                if (response.IsValid)
                {
                    this.AddRememberMeCookie(response);
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }
            return RedirectAsUnauthorised();
        }

        private dynamic GuestLogin()
        {
            var request = this.Bind<GuestLoginRequest>();
            if (request != null)
            {
                var response = _loginHandler.Handle(request);
                if (response.IsValid)
                {
                    AddRememberMeCookie(response);
                    this.Session["user-events"] = _userEventQueries.GetEventIdsUserHasAccessTo(response.Id);
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }
            return RedirectAsUnauthorised();
        }

        private Response RedirectAsUnauthorised()
        {
            return this.Response.AsRedirect("/", RedirectResponse.RedirectType.SeeOther)
                            .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        private Response AddRememberMeCookie(LoginResponse response)
        {
            return this.Login(response.Id, DateTime.Now.AddDays(7), "/");
        }
    }
}

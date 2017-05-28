using benandkatiegetmarried.DAL.UserEvents;
using benandkatiegetmarried.UseCases;
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
        private IHandler<GuestLoginRequest, GuestLoginResponse> _GuestLoginHandler;
        private IHandler<UserLoginRequest, UserLoginResponse> _UserLoginHandler;
        private IUserQueries _userEventQueries;

        public RootModule(IHandler<GuestLoginRequest, GuestLoginResponse> guestLoginHandler
            , IHandler<UserLoginRequest, UserLoginResponse> userLoginHandler
            , IUserQueries userEventQueries)
        {
            _GuestLoginHandler = guestLoginHandler;
            _UserLoginHandler = userLoginHandler;
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
            var request = this.Bind<UserLoginRequest>();
            if (request != null)
            {
                var response = _UserLoginHandler.Handle(request);
                if (response.IsValid)
                {
                    this.AddRememberMeCookie(response.UserId);
                    this.Session["user-eventIds"] = response.EventIds;
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
                var response = _GuestLoginHandler.Handle(request);
                if (response.IsValid)
                {
                    AddRememberMeCookie(response.InviteId);
                    this.Session["guest-eventId"] = response.EventId;
                    this.Session["guest-inviteId"] = response.InviteId;
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

        private Response AddRememberMeCookie(Guid id)
        {
            return this.Login(id , DateTime.Now.AddDays(7), "/");
        }
    }
}

using benandkatiegetmarried.Common.ErrorHandling;
using benandkatiegetmarried.DAL.UserEvents;
using benandkatiegetmarried.UseCases;
using benandkatiegetmarried.UseCases.Login;
using FluentValidation;
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
        private IValidator<GuestLoginRequest> _guestValidator;
        private IValidator<UserLoginRequest> _userValidator;
        private IUserQueries _userEventQueries;

        public RootModule(IHandler<GuestLoginRequest, GuestLoginResponse> guestLoginHandler
            , IHandler<UserLoginRequest, UserLoginResponse> userLoginHandler
            , IValidator<GuestLoginRequest> guestValidator
            , IValidator<UserLoginRequest> userValidator
            , IUserQueries userEventQueries)
        {
            _GuestLoginHandler = guestLoginHandler;
            _UserLoginHandler = userLoginHandler;
            _userEventQueries = userEventQueries;
            _guestValidator = guestValidator;
            _userValidator = userValidator;

            Get["/"] = _ => View["LandingPage"];
            Post["/user-login"] = _ => UserLogin();
            Post["/guest-login"] = _ => GuestLogin();
            Post["/logout"] = _ => Logout();
        }

        private dynamic Logout()
        {
            return this.LogoutAndRedirect("/");
        }

        private dynamic UserLogin()
        {
            var request = this.Bind<UserLoginRequest>();
            var userValidation = _userValidator.Validate(request);
            if (!userValidation.IsValid)
            {
                return ErrorResponse.ValidationError(userValidation.Errors);
            }

            var response = _UserLoginHandler.Handle(request);
            if (response.IsValid)
            {
                this.AddRememberMeCookie(response.UserId);
                this.Session["user-eventIds"] = response.EventIds;
                return HttpStatusCode.OK;
            }
            return RedirectAsUnauthorised();
        }

        private dynamic GuestLogin()
        {
            var request = this.Bind<GuestLoginRequest>();
            var guestValidation = _guestValidator.Validate(request);
            if (!guestValidation.IsValid)
            {
                return ErrorResponse.ValidationError(guestValidation.Errors);
            }

            var response = _GuestLoginHandler.Handle(request);
            if (response.IsValid)
            {
                AddRememberMeCookie(response.InviteId);
                this.Session["guest-eventId"] = response.EventId;
                this.Session["guest-inviteId"] = response.InviteId;
                return HttpStatusCode.OK;
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

using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.Login;
using benandkatiegetmarried.DAL.UserEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.UseCases.Login
{
    public class LoginHandler : IHandler<GuestLoginRequest, GuestLoginResponse> ,
        IHandler<UserLoginRequest, UserLoginResponse>
    {
        private ILoginQueries _queries;
        private ILoginCommands _commands;
        private IValidator<GuestLoginRequest> _guestValidator;
        private IValidator<UserLoginRequest> _userValidator;
        private IUserQueries _userQueries;

        public LoginHandler(ILoginQueries queries
            , ILoginCommands commands
            , IValidator<GuestLoginRequest> guestValidator
            , IValidator<UserLoginRequest> userValidator
            , IUserQueries userQueries)
        {
            _queries = queries;
            _commands = commands;
            _guestValidator = guestValidator;
            _userValidator = userValidator;
            _userQueries = userQueries;
        }
        public GuestLoginResponse Handle(GuestLoginRequest request)
        {
            var validationResult = _guestValidator.Validate(request);
            if (validationResult.IsValid)
            {
                var invite = _queries.GetInviteFromPassword(request.Password);
                if(invite != null)
                {
                    return new GuestLoginResponse() { IsValid = true, InviteId = invite.Id, EventId = invite.EventId };
                }
            }
            return new GuestLoginResponse() { IsValid = false };
        }

        public UserLoginResponse Handle(UserLoginRequest request)
        {
            var validationResult = _userValidator.Validate(request);
            if (validationResult.IsValid)
            {
                var userId = _queries.GetUserIdFromPasswordAndUsername(request.Username, request.Password);
                if (userId != null)
                {
                    var userEvents = _userQueries.GetEventIdsUserHasAccessTo(userId);
                    if(userEvents != null)
                    {
                        return new UserLoginResponse() { IsValid = true, UserId = userId , EventIds = userEvents};
                    }
                }
            }
            return new UserLoginResponse() { IsValid = false };
        }
    }
}

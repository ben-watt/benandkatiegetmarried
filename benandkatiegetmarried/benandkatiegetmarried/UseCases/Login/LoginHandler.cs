using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.UseCases.Login
{
    public class LoginHandler : IHandler<GuestLoginRequest, LoginResponse> ,
        IHandler<UserLoginRequest, LoginResponse>
    {
        ILoginQueries _queries;
        ILoginCommands _commands;
        IValidator<GuestLoginRequest> _guestValidator;
        IValidator<UserLoginRequest> _userValidator;

        public LoginHandler(ILoginQueries queries
            , ILoginCommands commands
            , IValidator<GuestLoginRequest> guestValidator
            , IValidator<UserLoginRequest> userValidator)
        {
            _queries = queries;
            _commands = commands;
            _guestValidator = guestValidator;
            _userValidator = userValidator;
        }
        public LoginResponse Handle(GuestLoginRequest request)
        {
            var validationResult = _guestValidator.Validate(request);
            if (validationResult.IsValid)
            {
                var invite = _queries.GetInviteFromPassword(request.Password);
                if(invite != null)
                {
                    return new LoginResponse() { IsValid = true, Id = invite.Id };
                }
            }
            return new LoginResponse() { IsValid = false };
        }

        public LoginResponse Handle(UserLoginRequest request)
        {
            var validationResult = _userValidator.Validate(request);
            if (validationResult.IsValid)
            {
                var userId = _queries.GetUserIdFromPasswordAndUsername(request.Username, request.Password);
                if (userId != null)
                {
                    return new LoginResponse() { IsValid = true, Id = userId };
                }
            }
            return new LoginResponse() { IsValid = false };
        }
    }
}

using benandkatiegetmarried.Common.Security;
using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.Login;
using benandkatiegetmarried.DAL.UserEvents;
using FluentValidation;
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
        private IUserQueries _userQueries;

        public LoginHandler(ILoginQueries queries
            , IUserQueries userQueries
            , ILoginCommands commands)
        {
            _queries = queries;
            _userQueries = userQueries;
            _commands = commands;
        }
        public GuestLoginResponse Handle(GuestLoginRequest request)
        {
            var invite = _queries.GetInviteFromSecurityCode(request.SecurityCode);
            if(invite == null)
                throw new ArgumentException("Invite does not exist");
            
            if (invite.LoginAttempts > 3)
                throw new UnauthorizedAccessException("Login attempts exceeded");

            if (!invite.Password.CheckPassword(request.Password))
            {
                _commands.UpdateFailedLoginAttempts<Models.Invite>(invite.Id);
                throw new UnauthorizedAccessException("Invalid Password");
            }

            return new GuestLoginResponse() { IsValid = true, InviteId = invite.Id, EventId = invite.EventId };
 
        }
        
        public UserLoginResponse Handle(UserLoginRequest request)
        {
            var userId = _queries.GetUserIdFromPasswordAndUsername(request.Username, request.Password);
            if (userId != Guid.Empty)
            {
                var userEvents = _userQueries.GetEventIdsUserHasAccessTo(userId);
                if(userEvents.Count() > 0)
                {
                    return new UserLoginResponse() { IsValid = true, UserId = userId , EventIds = userEvents };
                }
                return new UserLoginResponse() { IsValid = true, UserId = userId, EventIds = null };
            }
            throw new ArgumentException("User does not exist");
        }
    }
}

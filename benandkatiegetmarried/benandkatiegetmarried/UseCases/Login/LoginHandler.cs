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
        private IUserQueries _userQueries;

        public LoginHandler(ILoginQueries queries
            , IUserQueries userQueries)
        {
            _queries = queries;
            _userQueries = userQueries;
        }
        public GuestLoginResponse Handle(GuestLoginRequest request)
        {

            var invite = _queries.GetInviteFromPassword(request.Password);
            if(invite != null)
                return new GuestLoginResponse() { IsValid = true, InviteId = invite.Id, EventId = invite.EventId };
            throw new Exception("Could not find a valid invite");

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
            throw new Exception("User does not exist");
        }
    }
}

using benandkatiegetmarried.DAL;
using benandkatiegetmarried.DAL.Queries;
using Nancy.Authentication.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.UseCases
{
    public class LoginGuest : IRequestHandler<LoginRequest, LoginResponse>
    {
        private IQueryHandler<CheckInvite, Guid?> _CheckIfInviteIsValid;

        public LoginGuest(IQueryHandler<CheckInvite, Guid?> CheckIfInviteIsValid )
        {
            _CheckIfInviteIsValid = CheckIfInviteIsValid;
        }
        public LoginResponse Handle(LoginRequest request)
        {
            var query = new CheckInvite() { password = request.password };
            var result = _CheckIfInviteIsValid.Handle(query);
            if (result.HasValue)
            {
                return new LoginResponse();
            }
            return new LoginResponse();
        }
    }

    public class LoginResponse
    {
        public Guid InviteId { get; set; }
        public bool IsValid { get; set; }
    }

    public class LoginRequest : IRequest<LoginResponse>
    {
        public string password { get; set; }
    }
}

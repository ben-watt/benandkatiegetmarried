using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.UseCases.Login
{
    public class LoginHandler : IHandler<LoginRequest, LoginResponse>
    {
        ILoginQueries _queries;
        ILoginCommands _commands;
        IValidator<LoginRequest> _validator;

        public LoginHandler(ILoginQueries queries
            , ILoginCommands commands
            , IValidator<LoginRequest> validator)
        {
            _queries = queries;
            _commands = commands;
            _validator = validator;
        }
        public LoginResponse Handle(LoginRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid)
            {
                var invite = _queries.GetInviteFromPassword(request.Password);
                if(invite != null)
                {
                    return new LoginResponse() { IsValid = true, InviteId = invite.Id };
                }
            }
            return new LoginResponse() { IsValid = false };
        }
    }
}

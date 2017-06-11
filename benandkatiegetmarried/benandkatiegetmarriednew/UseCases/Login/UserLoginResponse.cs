using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace benandkatiegetmarried.UseCases.Login
{
    public class UserLoginResponse : LoginResponse
    {
        public Guid UserId { get; set; }
        public IEnumerable<Guid> EventIds { get; set; }
        public string Error { get; internal set; }
    }
}

using System;

namespace benandkatiegetmarried.UseCases.Login
{
    public class LoginResponse
    {
        public Guid InviteId { get; set; }
        public bool IsValid { get; set; }
    }
}
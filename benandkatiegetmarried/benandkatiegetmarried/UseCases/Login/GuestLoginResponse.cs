using System;

namespace benandkatiegetmarried.UseCases.Login
{
    public class GuestLoginResponse : LoginResponse
    {
        public Guid EventId { get; set; }
        public Guid InviteId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace benandkatiegetmarried.UseCases.Login
{
    public class GuestLoginResponse : LoginResponse
    {
        public Guid EventId { get; set; }
        public Guid InviteId { get; set; }
        public string Error { get; internal set; }
    }
}
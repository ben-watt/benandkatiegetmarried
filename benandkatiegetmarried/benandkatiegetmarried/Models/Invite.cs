using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Invite
    {
        public string Password { get; set; }
        public IList<Guest> Guests { get; set; }
        public string Greeting { get; set; }
        public Guid Id { get; set; }
    }
}
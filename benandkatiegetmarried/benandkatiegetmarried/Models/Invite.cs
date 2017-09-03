using benandkatiegetmarried.Common;
using benandkatiegetmarried.Common.Security;
using Nancy.Security;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Invite : IIdentity
    {
        private string _guestType;

        public Invite()
        {
            Id = Guid.NewGuid();
            this.UserName = Id.ToString();
            this.Claims = new List<string> { "Guest" };
        }
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string SecurityCode { get; set; }
        public string Password { get; set; }
        public string Greeting { get; set; }
        public string Type { get; set; }
        public int LoginAttempts { get; set; }
        [PetaPoco.Ignore]
        public string UserName { get; set; }
        [PetaPoco.Ignore]
        public IEnumerable<string> Claims {get; set;}
    }
}
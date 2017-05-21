using benandkatiegetmarried.Common;
using Nancy.Security;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Invite : IUserIdentity
    {
        private string _guestType;

        public Invite()
        {
            Id = Guid.NewGuid();
            this.Guests = new List<Guest>();
            this.UserName = Id.ToString();
        }
        public Guid Id { get; set; }
        public string Password { get; set; }
        public IList<Guest> Guests { get; set; }
        public string Greeting { get; set; }
        [PetaPoco.Ignore]
        public string UserName { get; set; }
        [PetaPoco.Ignore]
        public IEnumerable<string> Claims { get; set; }

        public void AssociateGuest(Guest guest)
        {
            if (Guests.Count == 0)
            {
                this.Guests.Add(guest);
                this._guestType = guest.Type;
            }
            else if (_guestType == guest.Type)
            {
                this.Guests.Add(guest);
            }
            else
            {
                throw new ArgumentException("Cannot add invalid guest to invite");
            }

        }
    }
}
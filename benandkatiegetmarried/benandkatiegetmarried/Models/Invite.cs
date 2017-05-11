using benandkatiegetmarried.Common;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Invite : IValid
    {
        private string _guestType;

        public Invite()
        {
            Id = Guid.NewGuid();
            this.Guests = new List<Guest>();
        }
        public string Password { get; set; }
        public IList<Guest> Guests { get; set; }
        public string Greeting { get; set; }
        public Guid Id { get; set; }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(Password) 
                || Guests.Count < 1)
            {
                return false;
            }
            return true;
        }

        public void AssociateGuest(Guest guest)
        {
            if (guest.IsValid() && Guests.Count == 0)
            {
                this.Guests.Add(guest);
                this._guestType = guest.Type;
            }
            else if (guest.IsValid() && _guestType == guest.Type)
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
using benandkatiegetmarried.Common;
using Nancy.Security;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Guest : IUserIdentity, IValid
    {
        public Guest()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName => throw new NotImplementedException();
        public IEnumerable<string> Claims => throw new NotImplementedException();
        public string Type { get; set; }

        public virtual bool IsValid()
        {
            if (String.IsNullOrEmpty(FirstName)
                || String.IsNullOrEmpty(Type))
            {
                return false;
            }
            return true;
        }
    }
}
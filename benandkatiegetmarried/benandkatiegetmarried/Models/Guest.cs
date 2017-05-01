using Nancy.Security;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Guest : IUserIdentity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName => throw new NotImplementedException();
        public IEnumerable<string> Claims => throw new NotImplementedException();
    }
}
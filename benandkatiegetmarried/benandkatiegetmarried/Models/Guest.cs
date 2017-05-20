using benandkatiegetmarried.Common;
using Nancy.Security;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class Guest
    {
        public Guest()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public bool IsFeatured { get; set; }
    }
}
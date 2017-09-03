using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;
using benandkatiegetmarried.Common.Security;

namespace benandkatiegetmarried.Models
{
    public class User : IIdentity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [PetaPoco.Ignore]
        public IEnumerable<string> Claims => new List<string> { "User" };
    }
}

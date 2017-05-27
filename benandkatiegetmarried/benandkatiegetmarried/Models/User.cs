using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;

namespace benandkatiegetmarried.Models
{
    [PetaPoco.TableName("Users")]
    public class User : IUserIdentity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Claims => new List<string> { "User" };
    }
}

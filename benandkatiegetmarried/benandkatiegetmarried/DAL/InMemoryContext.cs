using benandkatiegetmarried.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL
{
    public class InMemoryContext : IDbContext
    {
        public InMemoryContext()
        {
            this.Guests = new List<Guest>();
            this.Invites = new List<Invite>();
        }
        public IList<Guest> Guests { get; private set; }
        public IList<Invite> Invites { get; private set; }
    }

    public interface IDbContext
    {
        IList<Guest> Guests { get; }
        IList<Invite> Invites { get; }
    }
}

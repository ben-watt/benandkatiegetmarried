using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.Queries
{
    public class CheckInvite : IQuery<Guid?>
    {
        public string password { get; set; }
    }
    public class CheckIfInviteIsValid : QueryHandler<CheckInvite, Guid?>
    {
        public CheckIfInviteIsValid(IDbContext db) : base(db) {}
        public override Guid? Handle(CheckInvite query)
        {
            return db.Invites.Where(i => i.Password == query.password)?.FirstOrDefault()?.Id;
        }
    }
}

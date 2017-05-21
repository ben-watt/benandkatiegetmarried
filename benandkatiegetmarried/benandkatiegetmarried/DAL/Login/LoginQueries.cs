using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Security;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Login
{
    public class LoginQueries : ILoginQueries
    {
        IDatabase _db;
        public LoginQueries(IDatabase db)
        {
            _db = db;
        }
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            Models.Invite invite;
            using (var uow = _db.GetTransaction())
            {
                invite = _db.FirstOrDefault<Models.Invite>("WHERE Id = @0", identifier);
                uow.Complete();
            }
            return invite;
        }

        public Models.Invite GetInviteFromPassword(string password)
        {
            Models.Invite invite;
            using(var uow = _db.GetTransaction())
            {
                invite = _db.FirstOrDefault<Models.Invite>("WHERE Password = @0", password);
                uow.Complete();
            }
            return invite;
        }
    }
}

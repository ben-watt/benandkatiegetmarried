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

        public Guid GetUserIdFromPasswordAndUsername(string username, string password)
        {
            Guid userId;
            using(var uow = _db.GetTransaction())
            {
                userId = _db.FirstOrDefault<Guid>(
                    @"SELECT Id 
                      FROM core.Users 
                      WHERE username = @0 AND password = @1"
                    , username.ToLower(), password);
                uow.Complete();
            }
            return userId;
        }
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            if (context.Request.Path.Contains("userLogin"))
            {
                return GetFromIdentifier<Models.User>(identifier);
            }
            return GetFromIdentifier<Models.Invite>(identifier);
        }

        private IUserIdentity GetFromIdentifier<T>(Guid identifier) where T : IUserIdentity
        {
            T entity;
            using (var uow = _db.GetTransaction())
            {
                entity = _db.FirstOrDefault<T>("WHERE Id = @0", identifier);
            }
            return entity;
        }

    }
}

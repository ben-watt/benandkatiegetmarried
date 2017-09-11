using System;
using Nancy;
using Nancy.Security;
using PetaPoco;
using benandkatiegetmarried.Common.Security;
using benandkatiegetmarried.Models;

namespace benandkatiegetmarried.DAL.Login
{
    public class LoginQueries : ILoginQueries
    {
        private IDatabase _db;

        public LoginQueries(IDatabase db)
        {
            _db = db;
        }

        public Invite GetInviteFromSecurityCode(string securityCode)
        {
            Models.Invite invite;
            using(var uow = _db.GetTransaction())
            {
                invite = _db.FirstOrDefault<Invite>("WHERE SecurityCode = @0", securityCode);
                uow.Complete();
            }
            return invite;
        }

        public Guid GetUserIdFromPasswordAndUsername(string username, string password)
        {
            Tuple<Guid, string> result;
            using(var uow = _db.GetTransaction())
            {
                result = _db.FirstOrDefault<Tuple<Guid,string>>(
                    @"SELECT Id , password
                      FROM core.Users 
                      WHERE username = @0"
                    , username.ToLower());
                uow.Complete();
            }

            if (password.CheckPassword(result.Item2))
                return result.Item1;
            return Guid.Empty;

        }
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            if (context.Request.Url.Path.Contains("/guest/"))
            {
                return GetFromIdentifier<Invite>(identifier);
            }
            return GetFromIdentifier<User>(identifier);
        }

        private IUserIdentity GetFromIdentifier<T>(Guid identifier) where T : IUserIdentity
        {
            T entity;
            using (var uow = _db.GetTransaction())
            {
                entity = _db.FirstOrDefault<T>("WHERE Id = @0", identifier);
                uow.Complete();
            }
            return entity;
        }
    }
}

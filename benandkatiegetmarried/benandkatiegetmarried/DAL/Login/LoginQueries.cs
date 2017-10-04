using System;
using Nancy;
using Nancy.Security;
using PetaPoco;
using benandkatiegetmarried.Common.Security;
using benandkatiegetmarried.Models;
using benandkatiegetmarried.Common.Logging;

namespace benandkatiegetmarried.DAL.Login
{
    public class LoginQueries : ILoginQueries
    {
        private IWeddingDatabase _db;
        private ILogger _log;

        public LoginQueries(IWeddingDatabase db, ILogger log)
        {
            _db = db;
            _log = log;
        }

        public Invite GetInviteFromSecurityCode(string securityCode)
        {
            Invite invite;
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

            _log.Information($"Request Context HashCode: {context.GetHashCode()}");

            if (context.Request.Url.Path.Contains("/guest/"))
            {
                return GetFromIdentifier<Invite>(identifier);
            }
            return GetFromIdentifier<User>(identifier);
        }

        private IUserIdentity GetFromIdentifier<T>(Guid identifier) where T : IUserIdentity
        {
            _log.Information("Get From Identifier Called");
            _log.Information($"Database Hash: {_db.GetHashCode()}");
            _log.Information($"LoginQuery Hash: {this.GetHashCode()}");

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

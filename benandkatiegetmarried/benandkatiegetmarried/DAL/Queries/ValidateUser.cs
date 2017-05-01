using Nancy.Authentication.Forms;
using Nancy.Security;
using System;
using Nancy;
using benandkatiegetmarried.DAL;
using System.Linq;
using System.Collections.Generic;

namespace benandkatiegetmarried.Models
{
    public class ValidateUserHandler : IUserMapper
    {
        private IDbContext _db;
        public ValidateUserHandler(IDbContext db)
        {
            this._db = db;
        }
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            return this._db.Guests.Where(g => g.Id == identifier)?.FirstOrDefault();
        }
    }
}
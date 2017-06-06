using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.Login
{
    public class LoginCommands : ILoginCommands
    {
        private IDatabase _db;
        public LoginCommands(IDatabase db)
        {
            _db = db;
        }
        public void UpdateFailedLoginAttempts<T>(Guid Id)
        {
            using(var uow = _db.GetTransaction())
            {
                _db.Update<T>("SET LoginAttempts = LoginAttempts + 1" +
                    "WHERE Id = @0", Id);
                uow.Complete();
            }
        }
    }
}

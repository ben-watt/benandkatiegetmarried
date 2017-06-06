using Nancy.Authentication.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.Login
{
    public interface ILoginQueries : IUserMapper
    {
        Models.Invite GetInviteFromSecurityCode(string securityCode);
        Guid GetUserIdFromPasswordAndUsername(string username, string password);
    }
}

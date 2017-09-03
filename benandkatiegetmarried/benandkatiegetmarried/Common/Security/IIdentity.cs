using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Common.Security
{
    public interface IIdentity: IUserIdentity
    {
        Guid Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.UserEvents
{
    public interface IUserEventsQueries
    {
        IEnumerable<Guid> GetEventIdsUserHasAccessTo(Guid userId);
    }
}

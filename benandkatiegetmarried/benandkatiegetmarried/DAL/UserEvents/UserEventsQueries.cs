using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.UserEvents
{
    public class UserEventsQueries : IUserEventsQueries
    {
        IDatabase _db;
        public UserEventsQueries(IDatabase db)
        {
            _db = db;
        }
        public IEnumerable<Guid> GetEventIdsUserHasAccessTo(Guid userId)
        {
            IEnumerable<Guid> eventIds;
            using (var uow = _db.GetTransaction())
            {
                eventIds = _db.Query<Guid>(@"SELECT EventId 
                                             FROM core.UserEventMapping 
                                             WHERE UserId = @0", userId);
                uow.Complete();
            }
            return eventIds;
        }
    }
}

using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.BaseQueries
{
    public abstract class EventCrudQueries<T, TKey> : IEventCrudQueries<T, TKey>
    {
        protected IDatabase _db;

        public EventCrudQueries(IDatabase db)
        {
            this._db = db;
        }

        private IEnumerable<Guid> GetUserEventIds(Guid userId)
        {
            IEnumerable<Guid> result;
            using(var uow = _db.GetTransaction())
            {
                result = _db.Query<Guid>(
                    @"SELECT e.EventId
                        FROM core.UserEventMapping AS e
                            INNER JOIN core.Users AS u
                               ON e.UserId = e.EventId
                        WHERE u.UserId = @0", userId);

                uow.Complete();
            }
            return result;
        }

        public virtual IEnumerable<T> GetAll(Guid userId)
        {
            var eventIds = this.GetUserEventIds(userId);
            IEnumerable<T> result;
            using(var uow = _db.GetTransaction())
            {
                result = _db.Query<T>("WHERE EventId IN (@0)", eventIds);
                uow.Complete();
            }
            return result;
        }

        public virtual T GetById(TKey Id, Guid userId)
        {
            var eventIds = this.GetUserEventIds(userId);
            T result;
            using (var uow = _db.GetTransaction())
            {
                result = _db.SingleOrDefault<T>("WHERE Id = @0 AND EventId IN (@1)", Id, eventIds);
                uow.Complete();
            }
            return result;
        }
    }
}

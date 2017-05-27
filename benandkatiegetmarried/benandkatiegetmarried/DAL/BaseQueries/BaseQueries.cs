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
        public virtual IEnumerable<T> GetAll(IEnumerable<TKey> eventIds)
        {
            IEnumerable<T> result;
            using(var uow = _db.GetTransaction())
            {
                result = _db.Query<T>("WHERE EventId IN (@0)", eventIds);
                uow.Complete();
            }
            return result;
        }

        public virtual T GetById(TKey Id, IEnumerable<TKey> eventIds)
        {
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

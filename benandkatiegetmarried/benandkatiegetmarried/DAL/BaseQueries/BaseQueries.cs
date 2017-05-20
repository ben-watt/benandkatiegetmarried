using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.BaseQueries
{
    public abstract class BaseQueries<T, TKey> : ICrudQueries<T, TKey>
    {
        protected IDatabase _db;

        public BaseQueries(IDatabase db)
        {
            this._db = db;
        }
        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> result;
            using(var uow = _db.GetTransaction())
            {
                result = _db.Query<T>("");
                uow.Complete();
            }
            return result;
        }

        public virtual T GetById(TKey Id)
        {
            T result;
            using (var uow = _db.GetTransaction())
            {
                result = _db.SingleOrDefault<T>(Id);
                uow.Complete();
            }
            return result;
        }
    }
}

using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.BaseQueries
{
    public class BaseQueries<T, TKey> : IBaseQueries<T, TKey>
    {
        private IDatabase _db;

        public BaseQueries(IDatabase db)
        {
            this._db = db;
        }
        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result;
            using(var uow = _db.GetTransaction())
            {
                result = _db.Query<T>("");
                uow.Complete();
            }
            return result;
        }

        public T GetById(TKey Id)
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

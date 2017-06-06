using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.BaseCommands
{
    public abstract class CrudCommands<T, TKey> : ICrudCommands<T, TKey>
    {
        protected IDatabase _db;
        public CrudCommands(IDatabase db)
        {
            _db = db;
        }

        public virtual void Create(IEnumerable<T> entity)
        {
            using (var uow = _db.GetTransaction())
            {
                foreach (var e in entity)
                {
                    _db.Insert(e);
                }
                uow.Complete();
            }
        }

        public void Remove(TKey Id)
        {
            using (var uow = _db.GetTransaction())
            {
                _db.Delete<T>("WHERE Id = @0", Id);
                uow.Complete();
            }
        }

        public void Update(IEnumerable<T> entity)
        {
            using (var uow = _db.GetTransaction())
            {
                foreach (var e in entity)
                {
                    _db.Update(e);
                }
                uow.Complete();
            }
        }
    }
}

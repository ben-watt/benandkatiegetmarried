using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.BaseCommands
{
    public class BaseCommands<T> : IBaseCommands<T>
    {
        public IDatabase _db;
        public BaseCommands(IDatabase db)
        {
            _db = db;
        }
        public void Create(T entity)
        {
            using(var uow = _db.GetTransaction())
            {
                _db.Insert(entity);
                uow.Complete();
            }           
        }

        public void Delete(T entity)
        {
            using(var uow = _db.GetTransaction())
            {
                _db.Delete(entity);
                uow.Complete();
            }
        }

        public void Update(T entity)
        {
            using(var uow = _db.GetTransaction())
            {
                _db.Update(entity);
                uow.Complete();
            }
        }
    }
}

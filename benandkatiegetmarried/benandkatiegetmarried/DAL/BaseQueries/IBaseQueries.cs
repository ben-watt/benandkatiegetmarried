using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.BaseQueries
{
    public interface IEventCrudQueries<T, TKey>
    {
        IEnumerable<T> GetAll(Guid userId);
        T GetById(TKey Id, Guid userId);
    }
}

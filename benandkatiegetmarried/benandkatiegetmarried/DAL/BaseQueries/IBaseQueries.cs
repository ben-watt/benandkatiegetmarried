using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.BaseQueries
{
    public interface IBaseQueries<T, TKey>
    {
        IEnumerable<T> GetAll();
        T GetById(TKey Id);
    }
}

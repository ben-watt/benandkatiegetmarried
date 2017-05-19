using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.BaseCommands
{
    public interface ICrudCommands<T, TKey>
    {
        void Create(IEnumerable<T> entity);
        void Remove(TKey Id);
        void Update(IEnumerable<T> entity);
    }
}

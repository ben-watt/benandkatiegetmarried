using benandkatiegetmarried.DAL.BaseQueries;
using benandkatiegetmarried.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.Weddings.Query
{
    public interface IWeddingQueries : ICrudQueries<Wedding, Guid>
    {
    }
}

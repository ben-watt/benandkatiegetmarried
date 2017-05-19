using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.DAL.BaseQueries;
using benandkatiegetmarried.Models;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Weddings.Query
{
    public class WeddingQueries : BaseQueries<Wedding, Guid>, IWeddingQueries
    {
        public WeddingQueries(IDatabase db) : base(db)
        {
        }
    }
}

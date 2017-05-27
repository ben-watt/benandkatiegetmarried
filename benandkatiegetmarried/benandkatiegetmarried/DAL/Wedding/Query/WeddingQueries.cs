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
    public class WeddingQueries : CrudQueries<Wedding, Guid>, IWeddingQueries
    {
        public WeddingQueries(IDatabase db) : base(db) {}
        public override IEnumerable<Wedding> GetAll()
        {
            var query =
                @"SELECT *
                    FROM core.Events as e
                       INNER JOIN core.weddings AS w
                           ON e.""Id"" = w.""EventId""";

            var result = new List<Wedding>();

            using (var uow = _db.GetTransaction())
            {
                result = _db.Query<Wedding>(query).ToList();
                uow.Complete();
            }
            return result;
        }
    }
}

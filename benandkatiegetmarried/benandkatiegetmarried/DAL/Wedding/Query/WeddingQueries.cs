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
    public class WeddingQueries : EventCrudQueries<Wedding, Guid>, IWeddingQueries
    {
        public WeddingQueries(IDatabase db) : base(db) {}
        public override IEnumerable<Wedding> GetAll(IEnumerable<Guid> eventIds)
        {
            var query =
                @"  SELECT *
                    FROM core.Events as e
                       INNER JOIN core.weddings AS w
                           ON e.""Id"" = w.""EventId""
                    WHERE e.Id IN (@0)";

            var result = new List<Wedding>();

            using (var uow = _db.GetTransaction())
            {
                result = _db.Query<Wedding>(query, eventIds).ToList();
                uow.Complete();
            }
            return result;
        }
    }
}

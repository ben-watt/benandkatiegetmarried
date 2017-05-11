using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Weddings.Query
{
    public class WeddingQueries : IWeddingQueries
    {
        private IDatabase _db;

        public WeddingQueries(IDatabase db)
        {
            _db = db;
        }
        public IList<Wedding> GetAll()
        {
            return _db.Query<Wedding>("").ToList();
        }

        public Wedding GetById(Guid id)
        {
            return _db.SingleOrDefault<Wedding>(id);
        }
    }
}

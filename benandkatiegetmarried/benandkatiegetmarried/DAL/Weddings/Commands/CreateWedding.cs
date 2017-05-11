using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Weddings.Commands
{
    class WeddingCommands : IWeddingCommands
    {
        IDatabase _db;
        public WeddingCommands(IDatabase db)
        {
            _db = db;
        }
        public void Create(Models.Wedding wedding)
        {
            using (var uow = _db.GetTransaction())
            {
                _db.Insert(wedding);
                uow.Complete();
            }
        }

        public void UpdateWedding(Wedding wedding)
        {
            _db.Update(wedding);
        }
    }
}

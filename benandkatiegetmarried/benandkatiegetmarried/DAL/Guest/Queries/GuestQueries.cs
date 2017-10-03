using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Guest.Queries
{
    public class GuestQueries : BaseQueries.EventCrudQueries<Models.Guest, Guid>, IGuestQueries
    {
        public GuestQueries(IWeddingDatabase db) : base(db)
        {
        }
    }
}

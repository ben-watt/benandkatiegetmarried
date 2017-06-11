using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Weddings.Commands
{
    public class WeddingCommands : CrudCommands<Models.Wedding, Guid>, IWeddingCommands
    {
        public WeddingCommands(IDatabase db) : base(db)
        {
        }
    }
}

using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Guest.Commands
{
    public class GuestCommands : CrudCommands<Models.Guest, Guid>, IGuestCommands
    {
        public GuestCommands(IDatabase db) : base(db)
        {
        }
    }
}

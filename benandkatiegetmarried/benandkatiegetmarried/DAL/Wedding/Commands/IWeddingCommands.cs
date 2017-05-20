using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using benandkatiegetmarried.DAL.BaseCommands;

namespace benandkatiegetmarried.DAL.Weddings.Commands
{
    public interface IWeddingCommands : ICrudCommands<Wedding, Guid>
    { 
    }
}

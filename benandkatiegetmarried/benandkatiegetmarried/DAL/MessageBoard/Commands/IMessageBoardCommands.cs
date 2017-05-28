using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.MessageBoard.Commands
{
    public interface IMessageBoardCommands 
        : BaseCommands.ICrudCommands<Models.MessageBoard, Guid>
    {
        
    }
}

using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.MessageBoard.Commands
{
    public class MessageBoardCommands : CrudCommands<Models.MessageBoard, Guid>, IMessageBoardCommands
    {
        public MessageBoardCommands(IWeddingDatabase db) : base(db)
        {
        }
    }
}

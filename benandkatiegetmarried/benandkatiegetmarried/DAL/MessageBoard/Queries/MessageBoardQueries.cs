using benandkatiegetmarried.DAL.BaseQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.MessageBoard.Queries
{
    public class MessageBoardQueries : EventCrudQueries<Models.MessageBoard, Guid>, IMessageBoardQueries
    {
        public MessageBoardQueries(IWeddingDatabase db) : base(db)
        {
        }
    }
}

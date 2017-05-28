using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardCommands
{
    public interface IGuestMessageBoardCommands
    {
        void Create(Models.Message message);
        void Update(Models.Message message);
        void Remove(Guid Id);
        void Delete(Guid messageBoardId, Guid messageId);
    }
}

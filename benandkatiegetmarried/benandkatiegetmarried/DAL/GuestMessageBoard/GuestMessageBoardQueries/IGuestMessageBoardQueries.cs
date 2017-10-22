using benandkatiegetmarried.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardQueries
{
    public interface IGuestMessageBoardQueries
    {
        IEnumerable<Models.MessageBoard> GetMessageBoards(Guid eventId);
        IEnumerable<Message> GetMessages(Guid messageBoardId);
        IEnumerable<Message> GetMessagesFromInvite(Guid messageBoardId, Guid InviteId);
        IEnumerable<Models.MessageGuest> GetAttributions(IEnumerable<Guid> messageIds);
        IEnumerable<Models.MessageGuest> GetLikes(IEnumerable<Guid> messageIds);
    }
}

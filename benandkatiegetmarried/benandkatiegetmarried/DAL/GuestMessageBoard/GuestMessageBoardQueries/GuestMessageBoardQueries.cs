using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using PetaPoco;

namespace benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardQueries
{
    public class GuestMessageBoardQueries : IGuestMessageBoardQueries
    {
        IDatabase _db;
        public GuestMessageBoardQueries(IDatabase db)
        {
            _db = db;
        }
        public IEnumerable<Models.MessageBoard> GetMessageBoards(Guid eventId)
        {
            IEnumerable<Models.MessageBoard> resultSet;
            using (var uow = _db.GetTransaction())
            {
                resultSet = _db.Query<Models.MessageBoard>("WHERE EventId = @0", eventId);
                uow.Complete();
            }
            return resultSet;
        }

        public IEnumerable<Message> GetMessages(Guid messageBoardId)
        {
            IEnumerable<Message> resultSet;
            using (var uow = _db.GetTransaction())
            {
                resultSet = _db.Query<Models.Message>("WHERE MessageBoardId = @0", messageBoardId);
                uow.Complete();
            }
            return resultSet;
        }

        public IEnumerable<Message> GetMessagesFromInvite(Guid messageBoardId, Guid inviteId)
        {
            IEnumerable<Message> resultSet;
            using (var uow = _db.GetTransaction())
            {
                resultSet = _db.Query<Models.Message>("WHERE MessageBoardId = @0 AND InviteId = @1"
                    , messageBoardId, inviteId);
                uow.Complete();
            }
            return resultSet;
        }
    }
}

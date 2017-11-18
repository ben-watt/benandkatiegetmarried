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
        IWeddingDatabase _db;
        public GuestMessageBoardQueries(IWeddingDatabase db)
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
                resultSet = _db.Query<Message>(@"SELECT Id,
                                                    MessageBoardId,
                                                    Text,
                                                    Date,
                                                    Hierarchy.ToString() AS Hierarchy,
                                                    Hierarchy.GetLevel() AS HierarchyLevel
                                                 FROM core.Messages
                                                 WHERE MessageBoardId = @0", messageBoardId).ToList();
                uow.Complete();
            }
            return resultSet;
        }

        public IEnumerable<Message> GetMessagesFromInvite(Guid messageBoardId, Guid inviteId)
        {
            IEnumerable<Message> resultSet;
            using (var uow = _db.GetTransaction())
            {
                resultSet = _db.Query<Message>(@"SELECT Id,
                                                    MessageBoardId,
                                                    Text,
                                                    Date,
                                                    Hierarchy.ToString() AS Hierarchy,
                                                    Hierarchy.GetLevel() AS HierarchyLevel
                                                FROM core.Messages AS m
                                                    INNER JOIN core.MessageAttributions AS ma
	                                                    ON m.Id = ma.MessageId
                                                    INNER JOIN core.Guests AS g 
                                                WHERE MessageBoardId = @0 AND InviteId = @1",
                                                       messageBoardId, inviteId).ToList();
                uow.Complete();
            }
            return resultSet;
        }

        public IEnumerable<MessageGuest> GetLikes(IEnumerable<Guid> messageIds)
        {
            IEnumerable<MessageGuest> resultSet = new List<MessageGuest>();

            if (messageIds.Count() == 0)
                return resultSet;

            using (var uow = _db.GetTransaction())
            {
                resultSet = _db.Query<MessageGuest>(@"SELECT g.Id,
                                                        g.FirstName,
                                                        g.LastName,
                                                        g.InviteId,
                                                        l.MessageId
                                                        FROM core.likes AS l
                                                            INNER JOIN core.Guests AS g
	                                                           ON l.GuestId = g.Id
                                                        WHERE MessageId IN (@0)", messageIds).ToList();
                uow.Complete();
            }
            return resultSet;
        }

        public IEnumerable<MessageGuest> GetAttributions(IEnumerable<Guid> messageIds)
        {
            IEnumerable<MessageGuest> resultSet = new List<MessageGuest>();

            if (messageIds.Count() == 0)
                return resultSet;

            using (var uow = _db.GetTransaction())
            {
                resultSet = _db.Query<Models.MessageGuest>(@"SELECT g.Id,
                                                        g.FirstName,
                                                        g.LastName,
                                                        g.InviteId,
                                                        a.MessageId
                                                        FROM core.MessageAttributions AS a
                                                            INNER JOIN core.Guests AS g
	                                                           ON a.GuestId = g.Id
                                                        WHERE MessageId IN (@0)", messageIds).ToList();
                uow.Complete();
            }
            return resultSet;
        }
    }
}

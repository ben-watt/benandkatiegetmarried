using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using benandkatiegetmarried.Models;
using PetaPoco;

namespace benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardCommands
{
    public class GuestMessageBoardCommands : IGuestMessageBoardCommands
    {
        private IWeddingDatabase _db;
        public GuestMessageBoardCommands(IWeddingDatabase db)
        {
            _db = db;
        }
        public void Create(Message message)
        {
            using(var uow = _db.GetTransaction())
            {
                _db.Insert(message);
                uow.Complete();
            }
        }

        public void Delete(Guid messageBoardId, Guid messageId)
        {
            using (var uow = _db.GetTransaction())
            {
                _db.Delete(@"DELETE FROM core.messages WHERE messageBoardId = @0 AND Id = @1"
                    , messageBoardId.ToString() , messageId.ToString());
                uow.Complete();
            }
        }

        public void Remove(Guid Id)
        {
            using (var uow = _db.GetTransaction())
            {
                _db.Delete<Message>(Id);
                uow.Complete();
            }
        }

        public void Update(Message message)
        {
            using (var uow = _db.GetTransaction())
            {
                _db.Update(message);
                uow.Complete();
            }
        }
    }
}

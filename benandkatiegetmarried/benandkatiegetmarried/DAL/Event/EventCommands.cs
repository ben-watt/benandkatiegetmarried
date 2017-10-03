using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace benandkatiegetmarried.DAL.Event
{
    public class EventCommands<T> : IEventCommands<T> where T : Models.Event
    {
        private IWeddingDatabase _db;

        public EventCommands(IWeddingDatabase db)
        {
            _db = db;
        }

        public void Create(IEnumerable<T> events, Guid userId)
        {
            using(var uow = _db.GetTransaction())
            {
                foreach (var e in events)
                {
                    _db.Insert(e);
                    _db.Execute(@"INSERT INTO core.UserEventMapping (Id, UserId, EventId) VALUES
                                 (@0, @1, @2)"
                        , Guid.NewGuid().ToString()
                        , userId.ToString() 
                        , e.Id);
                }
                uow.Complete();
            }
        }
    }
}

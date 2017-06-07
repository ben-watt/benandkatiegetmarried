using benandkatiegetmarried.DAL.BaseCommands;
using System;
using System.Collections.Generic;

namespace benandkatiegetmarried.DAL.Event
{
    public interface IEventCommands<T> where T : Models.Event
    {
        void Create(IEnumerable<T> events, Guid userId);
    }
}
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.BaseQueries;
using System;

namespace benandkatiegetmarried.DAL.Guest.Commands
{
    public interface IGuestQueries : ICrudQueries<Models.Guest, Guid>
    { 
    }
}
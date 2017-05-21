using benandkatiegetmarried.DAL.BaseCommands;
using System;

namespace benandkatiegetmarried.DAL.Guest.Commands
{
    public interface IGuestCommands : ICrudCommands<Models.Guest, Guid>
    {
    }
}
using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.MessageBoard.Commands;
using benandkatiegetmarried.DAL.MessageBoard.Queries;
using benandkatiegetmarried.Models;
using FluentValidation;
using Nancy.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules.EventDetailModules
{
    public class MessageBoardModule : EventDetailsBaseModule<MessageBoard, Guid>
    {
        public MessageBoardModule(IMessageBoardQueries _queries
            , IMessageBoardCommands _commands
            , IValidator<MessageBoard> _validator
            , ISession session) 
            : base("messageboards", _queries, _commands, _validator, session) {}
    }
}

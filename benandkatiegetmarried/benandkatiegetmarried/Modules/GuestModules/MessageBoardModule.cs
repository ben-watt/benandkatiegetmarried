using benandkatiegetmarried.Common.ModuleExtensions;
using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardCommands;
using benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardQueries;
using benandkatiegetmarried.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.Modules.GuestModules
{
    public class GuestMessageBoard : NancyModule
    { 
        private IGuestMessageBoardQueries _queries;
        private IGuestMessageBoardCommands _commands;
        private IValidator<Message> _messageValidator;

        public GuestMessageBoard(IGuestMessageBoardQueries queries
            , IGuestMessageBoardCommands commands
            , IValidator<Message> messageValidator) : base("api/messageboard")
        {
            this.RequiresAuthentication();
            this.RequiresClaims("Guest");

            _queries = queries;
            _commands = commands;
            _messageValidator = messageValidator;

            Get["/"] = _ => GetMessageBoards();
            Get["/{messageBoardId}/messages"] = p => GetMessages(p.messageBoardId);
            Post["/{id}"] = _ => PostMessage();
            Delete["/{messageBoardId}/messages/{messageId}"] = p => DeleteMessage(p.messageBoardId, p.messageId);
            Put["/{messageBoardId}/messages/{messageId}"] = _ => UpdateMessage();
        }

        private dynamic GetMessages(dynamic id)
        {
            var inviteQuery = this.Request.Query["for-invite"];
            if (inviteQuery != null)
            {
                return GetMessagesForInviteFromBoard(id, inviteQuery);
            }
            return GetAllMessagesOnBoard(id);
        }

        private dynamic GetMessagesForInviteFromBoard(dynamic messageBoardId, dynamic inviteQuery)
        {
            if(IsGuid(messageBoardId) && IsGuid(inviteQuery))
            {
                var response = _queries.GetMessagesFromInvite(messageBoardId, inviteQuery);
                return response;
            }
            return HttpStatusCode.BadRequest;
        }
        private dynamic GetAllMessagesOnBoard(dynamic id)
        {
            if (IsGuid(id))
            {
                var response = _queries.GetMessages(id);
                return response;
            }
            return HttpStatusCode.BadRequest;
        }

        private bool IsGuid(dynamic possibleGuid)
        {
            Guid result;
            if (Guid.TryParse(possibleGuid, out result))
            {
                return true;
            }
            return false;
        }

        private dynamic GetMessageBoards()
        {
            Guid? eventId = this.GetIdFromSession("guest-eventId");
            if(eventId != null)
            {
                var response = _queries.GetMessageBoards((Guid)eventId);
                return response;
            }
            return new TextResponse("No eventId in the session, unable to process request")
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        private dynamic UpdateMessage()
        {
            var request = this.Bind<Message>();
            var validationResult = _messageValidator.Validate(request);
            if (validationResult.IsValid)
            {
                _commands.Update(request);
                return HttpStatusCode.NoContent;
            }
            return HttpStatusCode.BadRequest;
        }

        private object DeleteMessage(dynamic messageBoardId, dynamic messageId)
        {
            if(IsGuid(messageBoardId) && IsGuid(messageId))
            {
                _commands.Delete((Guid)messageBoardId, (Guid)messageId);
                return HttpStatusCode.NoContent;
            }
            return HttpStatusCode.BadRequest;
        }

        private dynamic PostMessage()
        {
            var request = this.Bind<Message>();
            var validationResult = _messageValidator.Validate(request);
            if (validationResult.IsValid)
            {
                _commands.Create(request);
                return HttpStatusCode.NoContent;
            }
            return HttpStatusCode.BadRequest;
        }

    }
}


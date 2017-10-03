using benandkatiegetmarried.Common.JsonSerialization;
using benandkatiegetmarried.Common.ModuleService;
using benandkatiegetmarried.Common.Validation;
using benandkatiegetmarried.DAL;
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.BaseQueries;
using benandkatiegetmarried.DAL.Event;
using benandkatiegetmarried.DAL.Guest.Commands;
using benandkatiegetmarried.DAL.Guest.Queries;
using benandkatiegetmarried.DAL.GuestEventDetails.Queries;
using benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardCommands;
using benandkatiegetmarried.DAL.GuestMessageBoard.GuestMessageBoardQueries;
using benandkatiegetmarried.DAL.Login;
using benandkatiegetmarried.DAL.MessageBoard.Commands;
using benandkatiegetmarried.DAL.MessageBoard.Queries;
using benandkatiegetmarried.DAL.Rsvp.RsvpCommands;
using benandkatiegetmarried.DAL.Rsvp.RsvpQueries;
using benandkatiegetmarried.DAL.UserEvents;
using benandkatiegetmarried.DAL.Venue.VenueCommands;
using benandkatiegetmarried.DAL.Venue.VenueQueries;
using benandkatiegetmarried.DAL.Weddings.Commands;
using benandkatiegetmarried.DAL.Weddings.Query;
using benandkatiegetmarried.Models;
using benandkatiegetmarried.UseCases;
using benandkatiegetmarried.UseCases.Login;
using benandkatiegetmarried.UseCases.Rsvp;
using FluentValidation;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Newtonsoft.Json;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried
{
    public class ContainerRegistration : IRegistrations
    {
        public IEnumerable<TypeRegistration> TypeRegistrations {
            get {

                return new[] {
                    Register.PerRequest<IHandler<GuestLoginRequest, GuestLoginResponse>, LoginHandler>(),
                    Register.PerRequest<IHandler<UserLoginRequest, UserLoginResponse>, LoginHandler>(),
                    Register.PerRequest<IHandler<RsvpRequest, UseCases.Rsvp.RsvpResponse>, RsvpHandler>(),
                    Register.PerRequest<IValidator<Guest>, GuestValidator>(),
                    Register.PerRequest<IValidator<Invite>, Common.Validation.IValidator>(),
                    Register.PerRequest<IValidator<Venue>, VenueValidator>(),
                    Register.PerRequest<IValidator<Message>, MessageValidator>(),
                    Register.PerRequest<IValidator<Wedding>, WeddingValidator>(),
                    Register.PerRequest<IValidator<UserLoginRequest>, UserLoginValidator>(),
                    Register.PerRequest<IValidator<GuestLoginRequest>, GuestLoginValidator>(),
                    Register.PerRequest<IValidator<Rsvp>, RsvpValidator>(),
                    Register.PerRequest<IValidator<MessageBoard>, MessageBoardValidator>(),
                    Register.PerRequest<IGuestEventDetailsQueries<Guid>, GuestEventDetailsQueries<Guid>>(),
                    Register.PerRequest<IEventCommands<Wedding>, EventCommands<Wedding>>(),
                    Register.PerRequest<JsonSerializer, CustomJsonSerializer>(),
                    Register.PerRequest<IWeddingDatabase, WeddingDatabase>(),
                    Register.PerRequest<IEventCommands<Event>, EventCommands<Event>>(),
                    Register.PerRequest<IGuestCommands, GuestCommands>(),
                    Register.PerRequest<IGuestQueries, GuestQueries>(),
                    Register.PerRequest<IGuestMessageBoardQueries, GuestMessageBoardQueries>(),
                    Register.PerRequest<IGuestMessageBoardCommands, GuestMessageBoardCommands>(),
                    Register.PerRequest<ILoginQueries, LoginQueries>(),
                    Register.PerRequest<ILoginCommands, LoginCommands>(),
                    Register.PerRequest<IMessageBoardCommands,MessageBoardCommands>(),
                    Register.PerRequest<IMessageBoardQueries, MessageBoardQueries>(),
                    Register.PerRequest<IRsvpCommands, RsvpCommands>(),
                    Register.PerRequest<IRsvpQueries, RsvpQueries>(),
                    Register.PerRequest<IUserQueries, UserQueries>(),
                    Register.PerRequest<IVenueQueries, VenueQueries>(),
                    Register.PerRequest<IVenueCommands, VenueCommands>(),
                    Register.PerRequest<IWeddingCommands, WeddingCommands>(),
                    Register.PerRequest<IWeddingQueries, WeddingQueries>()
                };
            }
           
        }

        private int UserQueries()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations { get; private set; }

        public IEnumerable<InstanceRegistration> InstanceRegistrations { get; private set; }
    }
}

using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Conventions;
using System.IO;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.Authentication.Forms;
using Nancy.ViewEngines;
using benandkatiegetmarried.DAL;
using PetaPoco;
using benandkatiegetmarried.DAL.BaseQueries;
using benandkatiegetmarried.Models;
using benandkatiegetmarried.DAL.Weddings.Query;
using benandkatiegetmarried.DAL.BaseCommands;
using benandkatiegetmarried.DAL.Weddings.Commands;
using benandkatiegetmarried.Common.Validation;
using FluentValidation;
using Nancy.Session;
using benandkatiegetmarried.UseCases;
using benandkatiegetmarried.UseCases.Login;
using benandkatiegetmarried.DAL.GuestEventDetails.Queries;
using benandkatiegetmarried.UseCases.Rsvp;
using benandkatiegetmarried.Common.JsonSerialization;
using Newtonsoft.Json;
using benandkatiegetmarried.Common.ErrorHandling;
using benandkatiegetmarried.DAL.Event;
using Nancy.Session.InProc;
using Nancy.Cryptography;

namespace benandkatiegetmarried
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof(IDatabase), WeddingDatabaseBuilder.Default());
            container.Register(typeof(IHandler<GuestLoginRequest, GuestLoginResponse>), typeof(LoginHandler));
            container.Register(typeof(IHandler<UserLoginRequest, UserLoginResponse>), typeof(LoginHandler));
            container.Register(typeof(IHandler<RsvpRequest, RsvpResponse>), typeof(RsvpHandler));
            container.Register(typeof(IValidator<Guest>), typeof(GuestValidator));
            container.Register(typeof(IValidator<Invite>), typeof(Common.Validation.IValidator));
            container.Register(typeof(IValidator<Venue>), typeof(VenueValidator));
            container.Register(typeof(IValidator<Message>), typeof(MessageValidator));
            container.Register(typeof(IValidator<Wedding>), typeof(WeddingValidator));
            container.Register(typeof(IValidator<UserLoginRequest>), typeof(UserLoginValidator));
            container.Register(typeof(IValidator<GuestLoginRequest>), typeof(GuestLoginValidator));
            container.Register(typeof(IValidator<RSVP>), typeof(RsvpValidator));
            container.Register(typeof(IValidator<MessageBoard>), typeof(MessageBoardValidator));
            container.Register(typeof(IGuestEventDetailsQueries<Guid>), typeof(GuestEventDetailsQueries<Guid>));
            container.Register<JsonSerializer, CustomJsonSerializer>();
            container.Register(typeof(ISession), new Session());
            container.Register(typeof(IEventCommands<Wedding>), typeof(EventCommands<Wedding>));
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var cryptoConfig = new CryptographyConfiguration(
                new RijndaelEncryptionProvider(
                        new PassphraseKeyGenerator("dodeda29fn2k191aed;foim!92cc0z9ldAZZZEgtjkalpoeid", new byte[] { 4, 6, 3, 6, 3, 9, 5 , 3 })),
                new DefaultHmacProvider(
                        new PassphraseKeyGenerator("oewcn38203ejei0dnmk3o£Q£RFAaru92ofoj", new byte[] { 5, 7, 2, 6, 6, 3 , 5 , 4}))
                        );

            var authConfig = new FormsAuthenticationConfiguration()
            {
                DisableRedirect = true, 
                UserMapper = container.Resolve<IUserMapper>()
            };

            FormsAuthentication.Enable(pipelines, authConfig);
            pipelines.EnableInProcSessions();
            ErrorHandling.Enable(pipelines);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            var pipe = pipelines;

            pipelines.BeforeRequest.InsertItemAtPipelineIndex(3, (ctx) =>
            {
                container.Register(typeof(ISession), ctx.Request.Session);
                return null;
            });
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.ViewLocationConventions.Add((viewName, model, ctx) =>
            {
                return Path.Combine("Views", viewName);
            });
        }
    }
}

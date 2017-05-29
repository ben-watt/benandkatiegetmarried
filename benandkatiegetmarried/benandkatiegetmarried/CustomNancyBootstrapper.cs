﻿using Nancy;
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
        }
        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var authConfig = new FormsAuthenticationConfiguration()
            {
                RedirectUrl = "~/",
                UserMapper = container.Resolve<IUserMapper>()
            };
            CookieBasedSessions.Enable(pipelines);
            FormsAuthentication.Enable(pipelines, authConfig);
            ErrorHandling.Enable(pipelines);
        }
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.ViewLocationConventions.Add((viewName, model, ctx) =>
            {
                return Path.Combine("Views", viewName);
            });
        }
        protected override IRootPathProvider RootPathProvider => new CustomRootPathProvider();
    }

    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Path.GetFullPath(Path.Combine("..", ".."));
        }
    }
}

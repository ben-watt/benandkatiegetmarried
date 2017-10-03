using Nancy;
using System;
using Nancy.Conventions;
using System.IO;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.Authentication.Forms;
using benandkatiegetmarried.DAL;
using PetaPoco;
using benandkatiegetmarried.Models;
using benandkatiegetmarried.Common.Validation;
using FluentValidation;
using benandkatiegetmarried.UseCases;
using benandkatiegetmarried.UseCases.Login;
using benandkatiegetmarried.DAL.GuestEventDetails.Queries;
using benandkatiegetmarried.UseCases.Rsvp;
using benandkatiegetmarried.Common.JsonSerialization;
using Newtonsoft.Json;
using benandkatiegetmarried.Common.ErrorHandling;
using benandkatiegetmarried.DAL.Event;
using Nancy.Cryptography;
using benandkatiegetmarried.Common.ModuleService;
using Nancy.Hosting.Aspnet;
using System.Collections.Generic;
using System.Reflection;
using benandkatiegetmarried.DAL.Login;

namespace benandkatiegetmarried
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {

        protected override TinyIoCContainer GetApplicationContainer()
        {
            return new TinyIoCContainer();
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var cryptoConfig = new CryptographyConfiguration(
                new RijndaelEncryptionProvider(
                        new PassphraseKeyGenerator("dodeda29fn2k191aed;foim!92cc0z9ldAZZZEgtjkalpoeid", new byte[] { 4, 6, 3, 6, 3, 9, 5, 3 })),
                new DefaultHmacProvider(
                        new PassphraseKeyGenerator("oewcn38203ejei0dnmk3o£Q£RFAaru92ofoj", new byte[] { 5, 7, 2, 6, 6, 3, 5, 4 }))
                        );

            var authConfig = new FormsAuthenticationConfiguration()
            {
                CryptographyConfiguration = cryptoConfig,
                DisableRedirect = true,
                UserMapper = container.Resolve<ILoginQueries>()
            };          
            
            FormsAuthentication.Enable(pipelines, authConfig);
            ErrorHandling.Enable(pipelines);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                            .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, X-Requested-With");
            });

            var db = container.Resolve<IWeddingDatabase>();

            var modules = this.GetAllModules(context);
            container.Register(typeof(IModuleService), new ModuleService(modules));
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

using Nancy;
using System;
using Nancy.Conventions;
using System.IO;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.Authentication.Forms;
using benandkatiegetmarried.Common.ErrorHandling;
using Nancy.Cryptography;
using benandkatiegetmarried.Common.ModuleService;
using benandkatiegetmarried.DAL.Login;
using Newtonsoft.Json;
using benandkatiegetmarried.Common.Logging;

namespace benandkatiegetmarried
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<JsonSerializer, CustomJsonSerializer>();
        }
        protected override TinyIoCContainer GetApplicationContainer()
        {
            return new TinyIoCContainer();
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

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
            ErrorHandling.Enable(pipelines, container.Resolve<ILogger>());

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

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

namespace benandkatiegetmarried
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof(IDatabase), WeddingDatabaseBuilder.Default());
            container.Register(typeof(Common.Validation.IValidator<Venue>), new VenueValidator());
            container.Register(typeof(Common.Validation.IValidator<Wedding>), new WeddingValidator());
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

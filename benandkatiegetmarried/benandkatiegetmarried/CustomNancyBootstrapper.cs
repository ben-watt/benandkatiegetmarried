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
using benandkatiegetmarried.UseCases;
using benandkatiegetmarried.DAL;
using benandkatiegetmarried.DAL.Queries;

namespace benandkatiegetmarried
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof(IRequestHandler<LoginRequest,LoginResponse>), typeof(LoginGuest));
            container.Register(typeof(IQueryHandler<CheckInvite, Guid?>), typeof(CheckIfInviteIsValid));
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var authConfig = new FormsAuthenticationConfiguration()
            {
                RedirectUrl = "~/",
                UserMapper = container.Resolve<IUserMapper>()
            };
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

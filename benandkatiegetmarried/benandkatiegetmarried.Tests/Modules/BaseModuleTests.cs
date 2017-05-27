using benandkatiegetmarried.Models;
using Moq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarriedTests.Modules
{
    public abstract class BaseModuleTests<TModule
        , TQuerys
        , TCommands
        , TValidator
        , TEntity> 
        where TModule : NancyModule
        where TQuerys : class
        where TCommands : class
        where TValidator : class
    {
        protected Mock<TQuerys> _queries = new Mock<TQuerys>();
        protected Mock<TCommands> _commands = new Mock<TCommands>();
        protected Mock<TValidator> _validator = new Mock<TValidator>();
        protected ConfigurableBootstrapper _bootstrapper => DefaultBootstrapper();

        public BaseModuleTests() {}

        protected Browser GetApiBrowser(ConfigurableBootstrapper bootstrapper)
        {
            return new Browser(bootstrapper, x => x.Header("Accept", "application/json"));
        }

        protected ConfigurableBootstrapper DefaultBootstrapper()
        {
            return new ConfigurableBootstrapper((config) => {
                config.Module<TModule>()
                       .Dependency(_queries.Object)
                       .Dependency(_commands.Object)
                       .Dependency(_validator.Object);
            });
        }
    }

    public static class BootstrapperExtensions
    {
        public static ConfigurableBootstrapper WithLoggedInUser(
            this ConfigurableBootstrapper bootstrapper
            , string userName
            , IEnumerable<Guid> userEventIds)
        {

            var session = new Dictionary<string, object>();
            session.Add("user-events", userEventIds);

            bootstrapper.BeforeRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.CurrentUser = new User() { UserName = userName };
                ctx.Request.Session = new Session(session);
                return null;
            });

            return bootstrapper;
        }
    }
}

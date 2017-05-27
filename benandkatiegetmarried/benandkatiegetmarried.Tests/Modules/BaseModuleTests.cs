using benandkatiegetmarried.Models;
using Moq;
using Nancy;
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

        public BaseModuleTests() {}

        protected Browser GetApiBrowser(ConfigurableBootstrapper bootstrapper)
        {
            return new Browser(bootstrapper, x => x.Header("Accept", "application/json"));
        }

        protected Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> BootstrapBuilder()
        {
            return (config) => config.Module<TModule>()
                       .Dependency(_queries.Object)
                       .Dependency(_commands.Object)
                       .Dependency(_validator.Object);
        }
    }

    public static class BootstrapperBuilder
    {
        public static Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> WithLoggedInUser(
            this Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> bootstrapConfig, string userName)
        {
            return bootstrapConfig += (config) => config.RequestStartup((container, pipeline, ctx) => ctx.CurrentUser = new User() { UserName = userName });
        }

        public static ConfigurableBootstrapper Build(this Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> config)
        {
            return new ConfigurableBootstrapper(config);
        }
    }
}

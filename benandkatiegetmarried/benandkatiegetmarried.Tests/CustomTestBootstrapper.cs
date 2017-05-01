using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Conventions;
using Nancy;
using Nancy.TinyIoc;

namespace benandkatiegetmarriedTests
{
    class CustomTestBootstrapper : ConfigurableBootstrapper
    { 
        public CustomTestBootstrapper() { }
        public CustomTestBootstrapper(Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> config)
            : base(config) {}
        protected override IRootPathProvider RootPathProvider => new TestRootPathProvider();
    }
}

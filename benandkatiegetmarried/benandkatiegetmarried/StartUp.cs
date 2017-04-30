using Nancy.Owin;
using Owin;

namespace benandkatiegetmarried
{
    internal class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
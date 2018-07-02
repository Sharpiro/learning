using Owin;

//[assembly: OwinStartup(typeof(SignalR.Web.Startup))]

namespace SignalR.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseOwin(pipeLine => pipeLine(next =>
            //{
            //    var builder = new AppBuilder();
            //    builder.MapSignalR();
            //    return builder.Build<Func<IDictionary<string, object>, Task>>();
            //}));
            app.MapSignalR();
        }
    }
}
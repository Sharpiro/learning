using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace InterviewPrep.Angular2FirstLook
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationEnvironment env)
        {
            app.UseIISPlatformHandler();

            app.UseRequestMiddleware();

            app.UseMvc(options => { options.MapRoute("DefaultApi", "api/{controller}/{action}/{id?}"); });

            app.UseNodeModules(env);

            app.UseFileServer();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

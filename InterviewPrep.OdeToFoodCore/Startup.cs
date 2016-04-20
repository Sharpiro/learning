using InterviewPrep.OdeToFoodCore.DataAccess;
using InterviewPrep.OdeToFoodCore.Entities;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewPrep.OdeToFoodCore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("config.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["database:connection"];
            services.AddMvc();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<FoodContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DbContext>(provider => new FoodContext(connectionString, ConnectionType.Sql));
            //services.AddTransient<DbContext, FoodContext>();
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, ConfigurationGreeter>();
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FoodContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, IGreeter greeter)
        {
            app.UseIISPlatformHandler();
            if (environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseFileServer();

            app.UseIdentity();
            app.UseMvc(routeBuilder => routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
using InterviewPrep.OdeToFoodRc2.DataAccess;
using InterviewPrep.OdeToFoodRc2.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewPrep.OdeToFoodRc2
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["database:connection"];
            services.AddMvc();
            services.AddDbContext<FoodContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DbContext>(provider => new FoodContext(connectionString, ConnectionType.Sql));
            //services.AddTransient<DbContext, FoodContext>();
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, ConfigurationGreeter>();
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FoodContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseFileServer();

            app.UseIdentity();
            app.UseMvc(routeBuilder => routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}

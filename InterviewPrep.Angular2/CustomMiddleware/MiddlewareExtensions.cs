using System.IO;
using InterviewPrep.Angular2.CustomMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNet.Builder
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, IHostingEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "node_modules");
            var options = new StaticFileOptions { FileProvider = new PhysicalFileProvider(path), RequestPath = new PathString("/node_modules")};
            app.UseStaticFiles(options);
            return app;
        }

        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RedirectMiddleware>();
            return app;
        }
    }
}
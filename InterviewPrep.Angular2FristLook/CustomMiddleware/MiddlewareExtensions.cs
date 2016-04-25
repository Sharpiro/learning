using System.IO;
using InterviewPrep.Angular2FristLook.CustomMiddleware;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.PlatformAbstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNet.Builder
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, IApplicationEnvironment env)
        {
            var path = Path.Combine(env.ApplicationBasePath, "node_modules");
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
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InterviewPrep.Angular2.CustomMiddleware
{
    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/dashboard")
            {
                context.Response.Redirect("/");
                return;
            }
            await _next.Invoke(context);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace InterviewPrep.Angular2FirstLook.CustomMiddleware
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
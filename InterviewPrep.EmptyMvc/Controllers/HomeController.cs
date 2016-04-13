using Microsoft.AspNet.Mvc;

namespace InterviewPrep.EmptyMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = User.Identity.Name;
            ViewBag.User = user;
            return View();
        }
    }
}

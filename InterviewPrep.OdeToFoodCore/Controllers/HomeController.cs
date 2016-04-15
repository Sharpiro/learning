using Microsoft.AspNet.Mvc;

namespace InterviewPrep.OdeToFoodCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreeter _greeter;

        public HomeController(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public string Index()
        {
            return _greeter.GetGreeting();
        }
    }
}
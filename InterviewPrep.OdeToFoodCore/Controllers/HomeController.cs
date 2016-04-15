using InterviewPrep.OdeToFoodCore.Models;
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

        public IActionResult Index()
        {
            var restaurant = new Restaurant { Id = 1, Name = "Sabatino's" };
            return View(restaurant);
            //return new ObjectResult(restaurant);
            //return Content(_greeter.GetGreeting());
        }

        public string Basic()
        {
            const string basicResponse = "basic response string";
            return basicResponse;
        }
    }
}
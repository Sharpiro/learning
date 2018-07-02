using Microsoft.AspNetCore.Mvc;

namespace InterviewPrep.OdeToFoodRc2.ViewComponents
{
    public class Greeting : ViewComponent
    {
        private readonly IGreeter _greeter;

        public Greeting(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public IViewComponentResult Invoke()
        {
            var model = _greeter.GetGreeting();
            return View("Default", model);
        }
    }
}

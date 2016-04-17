using InterviewPrep.OdeToFoodCore.DataAccess;
using InterviewPrep.OdeToFoodCore.Entities;
using InterviewPrep.OdeToFoodCore.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;

namespace InterviewPrep.OdeToFoodCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreeter _greeter;
        private readonly IFoodRepository<Restaurant> _restaurantRepository;

        public HomeController(IGreeter greeter, IFoodRepository<Restaurant> restaurantRepository)
        {
            if (greeter == null)
                throw new ArgumentNullException(nameof(greeter));
            if (restaurantRepository == null)
                throw new ArgumentNullException(nameof(restaurantRepository));
            _greeter = greeter;
            _restaurantRepository = restaurantRepository;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = _restaurantRepository.GetAll();
            model.CurrentGreeting = _greeter.GetGreeting();
            return View(model);
            //return new ObjectResult(restaurant);
            //return Content(_greeter.GetGreeting());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            var restaurant = new Restaurant { Name = model.Name, Cuisine = model.Cuisine };
            _restaurantRepository.Add(restaurant);
            return RedirectToAction("Details", new { id = restaurant.Id });
        }

        public IActionResult Details(int id)
        {
            var restaurant = _restaurantRepository.Get(id);
            if (restaurant == null)
                return RedirectToAction("Index");
            return View(restaurant);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            var restaurants = _restaurantRepository.GetAll();
            return restaurants;
        }
    }
}
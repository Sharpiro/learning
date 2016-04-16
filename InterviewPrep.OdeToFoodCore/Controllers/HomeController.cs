using InterviewPrep.OdeToFoodCore.Entities;
using InterviewPrep.OdeToFoodCore.Services;
using InterviewPrep.OdeToFoodCore.ViewModels;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;

namespace InterviewPrep.OdeToFoodCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreeter _greeter;
        private readonly IRestaurantRepository _restaurantRepository;

        public HomeController(IGreeter greeter, IRestaurantRepository restaurantRepository)
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
            var restaurant = new Restaurant { Name = model.Name, Cuisine = model.Cuisine };
            _restaurantRepository.Add(restaurant);
            return View("Details", restaurant);
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
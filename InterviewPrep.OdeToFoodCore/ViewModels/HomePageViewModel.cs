using InterviewPrep.OdeToFoodCore.Entities;
using System.Collections.Generic;

namespace InterviewPrep.OdeToFoodCore.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}

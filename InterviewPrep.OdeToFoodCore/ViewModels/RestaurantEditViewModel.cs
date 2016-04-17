using InterviewPrep.OdeToFoodCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace InterviewPrep.OdeToFoodCore.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MinLength(5)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
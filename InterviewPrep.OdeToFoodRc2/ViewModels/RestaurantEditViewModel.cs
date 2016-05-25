using InterviewPrep.OdeToFoodRc2.Entities;
using System.ComponentModel.DataAnnotations;

namespace InterviewPrep.OdeToFoodRc2.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MinLength(5), MaxLength(80)]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "please choose a cuisine type")]
        public CuisineType Cuisine { get; set; }
    }
}
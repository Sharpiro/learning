using System;
using InterviewPrep.OdeToFoodCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace InterviewPrep.OdeToFoodCore.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MinLength(5)]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "please choose a cuisine type")]
        public CuisineType Cuisine { get; set; }
    }
}
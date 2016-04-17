using InterviewPrep.OdeToFoodCore.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using InterviewPrep.OdeToFoodCore.DataAccess;

namespace InterviewPrep.OdeToFoodCore.Services
{
    public class InMemoryRestaurantRepository : IFoodRepository<Restaurant>
    {
        private IList<Restaurant> _restaurants;

        public InMemoryRestaurantRepository()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant {Id = 1, Name = "Tersigul's" },
                new Restaurant {Id = 2, Name = "LJ's and the Kat" },
                new Restaurant {Id = 3, Name = "King's Contrivance" }
            };
        }

        public void Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(restaurant);
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _restaurants = null;
        }

        public Restaurant Get(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            return restaurant;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }
    }
}

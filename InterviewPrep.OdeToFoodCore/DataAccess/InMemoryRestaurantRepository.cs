using System;
using System.Collections.Generic;
using System.Linq;
using InterviewPrep.OdeToFoodCore.Entities;

namespace InterviewPrep.OdeToFoodCore.DataAccess
{
    public class InMemoryRestaurantRepository : IRepository<Restaurant>
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

        public void Add(Restaurant entity)
        {
            entity.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(entity);
        }

        public int Commit()
        {
            return 0;
        }

        //public void Update(Restaurant restaurant)
        //{
        //    throw new NotImplementedException();
        //}

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

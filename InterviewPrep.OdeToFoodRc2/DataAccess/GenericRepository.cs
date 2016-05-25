using System.Linq;
using InterviewPrep.OdeToFoodRc2.Entities;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.OdeToFoodRc2.DataAccess
{
    public class GenericRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _set;

        public GenericRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
            _set = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        //public void Update(T restaurant)
        //{
        //    _set.Update(restaurant);
        //}

        public T Get(int id)
        {
            return _set.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _set;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
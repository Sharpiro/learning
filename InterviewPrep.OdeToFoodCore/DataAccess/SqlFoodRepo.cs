using Microsoft.Data.Entity;
using System.Linq;
using InterviewPrep.OdeToFoodCore.Entities;
using System.Collections.Generic;
using System;

namespace InterviewPrep.OdeToFoodCore.DataAccess
{
    public class SqlFoodRepo<T> : IFoodRepository<T> where T : class, IBaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _set;

        public SqlFoodRepo(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
            _set = _context.Set<T>();
        }

        public void Add(T restaurant)
        {
            _set.Add(restaurant);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

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
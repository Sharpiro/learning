using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.DesignPatterns
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _set;

        public GenericRepository(DbContext context)
        {
            _set = context.Set<T>();
        }

        public void Add(T data)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> func)
        {
            return _set.FirstOrDefault(func);
        }

        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> func)
        {
            return _set.Where(func);
        }
    }
}

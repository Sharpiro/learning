using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _set;

        public EFRepository(DbContext context)
        {
            _set = context.Set<T>();
        }

        public void Add(T data)
        {
            _set.Add(data);
        }

        public void Delete(T data)
        {
            _set.Remove(data);
        }

        public T Get(int id)
        {
            return _set.Find(id);
        }

        public T Get(Expression<Func<T, bool>> func)
        {
            return _set.Single(func);
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

using System;
using System.Linq;
using System.Data.Entity;

namespace InterviewPrep.Generics.RepositoryPattern
{
    public class CarRepository<T> : IRepository<T> where T : class
    {
        private DbContext _ctx;
        private DbSet<T> _set;

        public CarRepository(DbContext context)
        {
            _ctx = context;
            _set = _ctx.Set<T>();
        }

        public void Commit()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            _set.Add(entity);
        }
    }

    /// <summary>
    /// example of co-variance with the out keyword
    /// allows getting of children classes from parent repo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyRepository<out T> : IDisposable
    {
        T GetById(int id);
        IQueryable<T> GetAll();
    }

    /// <summary>
    /// example of contra-variance w/ the in keyword
    /// allows setting of parent classes from child repo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWriteOnlyRepository<in T> : IDisposable
    {
        void Insert(T entity);
        void Commit();
    }

    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T>
    {
    }
}

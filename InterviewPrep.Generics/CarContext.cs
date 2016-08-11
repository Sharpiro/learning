using InterviewPrep.Generics.Entities;
using System.Data.Entity;
using System;
using System.Linq;

namespace InterviewPrep.Generics
{
    public class CarContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

        public CarContext(string connectionString) : base()
        {
            //Database.SetInitializer<CarContext>(null);
            Database.SetInitializer(new DropCreateDatabaseAlways<CarContext>());
        }
    }

    public class SqlRepository<T> : IRepository<T> where T : class
    {
        private DbContext _ctx;
        private DbSet<T> _set;

        public SqlRepository(DbContext context)
        {
            _ctx = context;
            _set = _ctx.Set<T>();
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

    public interface IRepository<T>
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Insert(T entity);
        //void Commit();
    }
}

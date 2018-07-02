using System.Data.Entity;
using System.Linq;

namespace InterviewPrep.ServicePattern.DataLayer
{
    public class GenericRepository<T> where T : class
    {
        private readonly DbSet<T> _set;

        public GenericRepository(AutoContext context)
        {
            _set = context.Set<T>();
        }

        public T Get(int id)
        {
            return _set.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public void Update(T entity)
        {
            _set.Remove(entity);
        }
    }
}
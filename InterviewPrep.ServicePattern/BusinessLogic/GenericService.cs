using InterviewPrep.ServicePattern.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.ServicePattern.BusinessLogic
{
    public class GenericService<T> where T : class
    {
        private readonly AutoContext _context;
        private readonly DbSet<T> _set;
        //private readonly ContextFactory _contextFactory;

        //public GenericService(ContextFactory contextFactory)
        //{
        //    _contextFactory = contextFactory;
        //}

        public GenericService(AutoContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public T Get(int id)
        {
            return _set.Find(id);
        }

        public T Get(Expression<Func<T, bool>> func)
        {
            return _set.FirstOrDefault(func);
        }

        public IList<T> GetAll()
        {
            return _set.ToList();
        }

        public IList<T> GetAll(Expression<Func<T, bool>> func)
        {
            return _set.Where(func).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.DesignPatterns
{
    public interface IRepository<T>
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> func);
        void Add(T data);
    }
}

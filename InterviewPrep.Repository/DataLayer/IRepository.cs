using System;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Get(Expression<Func<T, bool>> func);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> func);
        void Add(T data);
        void Delete(T data);
    }
}
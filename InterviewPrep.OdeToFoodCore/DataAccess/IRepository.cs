using System;
using System.Collections.Generic;

namespace InterviewPrep.OdeToFoodCore.DataAccess
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        int Commit();
        //void Update(T restaurant);
    }

    //public interface IInMemoryRepository<T> : IFoodRepository<T>
    //{
    //    IEnumerable<T> GetAll();
    //}

    //public interface IEfRepository<T> : IFoodRepository<T>
    //{
    //    IQueryable<T> GetAll();
    //}
}

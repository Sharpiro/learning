using System;
using System.Collections.Generic;

namespace InterviewPrep.OdeToFoodCore.DataAccess
{
    public interface IFoodRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T restaurant);
        void Commit();
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

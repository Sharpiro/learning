using InterviewPrep.LinqFundamentals.DataLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.LinqFundamentals.DataLayer
{
    public interface IQueryStuff<T> : IEnumerable<T>
    {

    }

    public static class Extensions
    {
        //public static IQueryable<T> Count<T>(this IQueryable<T> queryable)
        //{
        //    var temp = Queryable.Count(queryable);
        //    throw new NotImplementedException();
        //}
    }

    public class FakeDataSet<T> : IQueryable<T>
    {
        public Type ElementType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Expression Expression
        {
            get
            {
                var expression = Expression.New(typeof(FakeDataSet<T>));
                return expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return new CustomQueryProvider();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class CustomQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var type = typeof(TResult);
            return default(TResult);
            throw new NotImplementedException();
        }
    }
}
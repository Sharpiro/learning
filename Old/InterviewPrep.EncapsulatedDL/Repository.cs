using InterviewPrep.EncapsulatedDL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.EncapsulatedDL
{
    public class Repository<T> where T : class, IEntity
    {
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            using (var context = new CommerceContext())
            {
                IQueryable<T> query = context.Set<T>().AsNoTracking();
                if (predicate != null)
                    query = query.Where(predicate);
                return query.ToList();
            }
        }

        public T Get(int id)
        {
            using (var context = new CommerceContext())
            {
                return context.Set<T>().AsNoTracking().SingleOrDefault(t => t.Id == id);
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            using (var context = new CommerceContext())
            {
                var entity = context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
                return entity;
            }
        }

        public void Add(T entity)
        {
            using (var context = new CommerceContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var context = new CommerceContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(T entity)
        {
            using (var context = new CommerceContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
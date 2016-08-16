using System;
using System.Linq;
using System.Linq.Expressions;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        private string _tableName;
        private SqlHelper _sqlHelper;

        public SqlRepository(SqlHelper sqlHelper, string tableName)
        {
            _sqlHelper = sqlHelper;
            _tableName = tableName;
        }

        public SqlRepository(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
            _tableName = $"{typeof(T).Name}s";
        }

        public T Get(int id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            var results = _sqlHelper.ExecuteQuery<T>(query, new { Id = id });
            var model = results.Single();
            return model;
        }

        public IQueryable<T> GetAll()
        {
            var query = $"SELECT * FROM {_tableName}";
            var results = _sqlHelper.ExecuteQuery<T>(query);
            return results.AsQueryable();
        }

        public T Get(Expression<Func<T, bool>> func)
        {
            throw new NotImplementedException();
        }

        public void Add(T data)
        {
            throw new NotImplementedException();
        }

        public void Delete(T data)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> func)
        {
            throw new NotImplementedException();
        }
    }
}

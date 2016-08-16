using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> ExecuteQuery<T>(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var results = connection.Query<T>(query, parameters, commandType: CommandType.Text);
                return results;
            }
        }
    }
}

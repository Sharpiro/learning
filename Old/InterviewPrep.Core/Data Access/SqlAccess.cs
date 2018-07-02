using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using System.Data.Entity;

namespace InterviewPrep.Core.Data_Access
{
    public static class SqlAccess
    {
        public static void ExecuteEfSqlCommand(string connectionString, string sqlString)
        {
            var config = new Config(connectionString);
            DbConfiguration.SetConfiguration(config);
            using (var context = new Context())
            {
                var roles = context.Users.ToList();
            }
        }

        public static void ExecuteDapper(string connectionString, string sqlString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<Role>(sqlString);
            }
        }

        public static void ExecuteSqlCommand(string connectionString, string sqlString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(sqlString, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                var columnNumber = reader.FieldCount;
                var roles = new List<Role>();
                while (reader.Read())
                {
                    var role = new Role
                    {
                        Id = (string)reader[0],
                        Name = (string)reader[1],
                        Discriminator = (string)reader[2]
                    };
                    Debug.WriteLine($"{reader[0]}");
                    roles.Add(role);
                }
            }
        }
    }
}

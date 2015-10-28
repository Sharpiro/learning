using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace InterviewPrep.Core.Data_Access
{
    public static class SqlAccess
    {
        public static void ExecuteEfSqlCommand(string connectionString, string sqlString)
        {
            Context.ConnectionString = connectionString;
            using (var context = new Context())
            {
                var roles = context.Users.ToList();
            }
        }

        public static void ExecuteSqlCommand(string connectionString, string sqlString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(sqlString, connection)
                {
                    CommandType = CommandType.Text
                };

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

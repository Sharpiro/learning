using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace InterviewPrep.Core.Data_Access
{
    [DbConfigurationType(typeof(Config))]
    public class Context : DbContext
    {
        public DbSet<Role> Users { get; set; }
        //public static string ConnectionString;

        public Context()
        {

        }
    }

    public class Config : DbConfiguration
    {
        public string ConnectionString { get; }

        public Config()
        {

        }

        public Config(string connectionString)
        {
            ConnectionString = connectionString;
            SetProviderServices("System.Data.SqlClient",
                System.Data.Entity.SqlServer.SqlProviderServices.Instance);
            var connectionFactory = new SqlConnectionFactory(ConnectionString);
            SetDefaultConnectionFactory(connectionFactory);
        }
    }
}

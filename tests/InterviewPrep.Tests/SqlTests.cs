using InterviewPrep.Core.Data_Access;
using Xunit;

namespace InterviewPrep.Tests
{
    public class SqlTests
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExistingDatabase;";

        [Fact]
        public void ExecuteEfSqlCommandTest()
        {
            SqlAccess.ExecuteEfSqlCommand(ConnectionString, "SELECT * FROM dbo.AspNetRoles");
        }

        [Fact]
        public void ExecuteDapperTest()
        {
            SqlAccess.ExecuteDapper(ConnectionString, "SELECT * FROM dbo.AspNetRoles");
        }

        [Fact]
        public void ExecuteSqlCommandTest()
        {
            SqlAccess.ExecuteSqlCommand(ConnectionString, "SELECT * FROM dbo.AspNetRoles");
        }
    }
}

using InterviewPrep.Core.Data_Access;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class SqlTests
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExistingDatabase;";

        [TestMethod]
        public void ExecuteEfSqlCommandTest()
        {
            SqlAccess.ExecuteEfSqlCommand(ConnectionString, "SELECT * FROM dbo.AspNetRoles");
        }

        [TestMethod]
        public void ExecuteDapperTest()
        {
            SqlAccess.ExecuteDapper(ConnectionString, "SELECT * FROM dbo.AspNetRoles");
        }

        [TestMethod]
        public void ExecuteSqlCommandTest()
        {
            SqlAccess.ExecuteSqlCommand(ConnectionString, "SELECT * FROM dbo.AspNetRoles");
        }
    }
}

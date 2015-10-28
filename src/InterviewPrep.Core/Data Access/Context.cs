using Microsoft.Data.Entity;

namespace InterviewPrep.Core.Data_Access
{
    public class Context : DbContext
    {
        public DbSet<Role> Users{ get; set; }
        public static string ConnectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}

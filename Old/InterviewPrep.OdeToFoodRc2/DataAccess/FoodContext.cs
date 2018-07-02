using System;
using InterviewPrep.OdeToFoodRc2.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.OdeToFoodRc2.DataAccess
{
    public class FoodContext : IdentityDbContext<User>
    {
        private readonly string _connectionString;
        private readonly ConnectionType _type;
        public DbSet<Restaurant> Restaurants { get; set; }

        public FoodContext()
        {
        }

        public FoodContext(string connectionString, ConnectionType type)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("test error", nameof(connectionString));
            if (type == ConnectionType.None)
                throw new ArgumentException("Must provide valid connection type", nameof(type));
            _connectionString = connectionString;
            _type = type;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_type == ConnectionType.Sql)
                optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
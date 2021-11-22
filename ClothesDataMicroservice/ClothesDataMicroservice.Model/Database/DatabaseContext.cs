using ClothesDataMicroservice.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClothesDataMicroservice.Model.DataBase
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<WeatherConditions> Conditions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        public string DbPath { get; private set; }

        public DatabaseContext()
        {
            this.DbPath = Environment.GetEnvironmentVariable("DATABASE_PATH");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging();

            options.UseNpgsql(DbPath);
        }
    }
}

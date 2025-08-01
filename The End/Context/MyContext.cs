using Microsoft.EntityFrameworkCore;
using The_End.Models;

namespace The_End.Context
{
    public class MyContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=./;Database=TheFinelDB;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }


    }

}

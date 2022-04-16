using ETicaret.Shared.Dal.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal
{
    public class MarketPlaceDbContext : DbContext, IDisposable
    {
        public static readonly string connectionString = "Server=localhost;Port=5432;Database=MarketPlace_Db;User Id=postgres;Password=root";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(connectionString);
        




            public DbSet<Category> Categories { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<User> Users { get; set; }

    }
    }




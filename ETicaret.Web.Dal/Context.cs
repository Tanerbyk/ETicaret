using ETicaret.Shared.Dal.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Dal
{
    public class Context : DbContext, IDisposable
    {
        public static readonly string connectionString = "server=DESKTOP-TOHAA3U\\SQLEXPRESS;database=ETicaretDb;integrated security = true;";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);

        }
        
         
           
            public DbSet<Category> Categories { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<User> Users { get; set; }

    }
    }




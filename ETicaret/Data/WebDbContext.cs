using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ETicaret.Shared.Data
{
    public class WebDbContext : IdentityDbContext
    {
        public WebDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Ignore<IdentityUserClaim<string>>();

            builder.Entity<IdentityUserRole<Guid>>().HasKey(x => new {x.UserId , x.RoleId});

            builder.Entity<IdentityUserClaim<string>>().HasKey(x => new {x.Id});

        }
        

    }
}

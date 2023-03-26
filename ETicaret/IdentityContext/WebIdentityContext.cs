
using ETicaret.Shared.Dal.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ETicaret.Web.IdentityContext
{
    public class WebIdentityContext:IdentityDbContext<WebUser>
    {
        public WebIdentityContext(DbContextOptions<WebIdentityContext> dbContext) : base(dbContext) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("customer");
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserClaim<string>>().HasKey(p => new { p.Id });


        }
    }
}

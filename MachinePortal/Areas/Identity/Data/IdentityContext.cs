using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MachinePortal.Models;

namespace MachinePortal.Models
{
    public class IdentityContext : IdentityDbContext<MachinePortalUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserPermission>()
                .HasKey(x => new { x.UserID, x.PermissionID });
        }

        public DbSet<Permission> Permission { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }
    }
}

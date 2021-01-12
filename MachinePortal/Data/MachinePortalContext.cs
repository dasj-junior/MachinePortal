using Microsoft.EntityFrameworkCore;
using MachinePortal.Models;

namespace MachinePortal.Models
{
    public class MachinePortalContext : DbContext
    {
        public MachinePortalContext(DbContextOptions<MachinePortalContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MachineResponsable>()
                .HasKey(x => new { x.MachineID, x.ResponsableID });

            modelBuilder.Entity<MachineDevice>()
                .HasKey(x => new { x.MachineID, x.DeviceID });
        }

        public DbSet<Asset> Asset { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Responsible> Responsible { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Area> Area { get; set; }
    }
}

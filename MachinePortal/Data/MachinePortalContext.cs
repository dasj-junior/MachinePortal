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
            modelBuilder.Entity<MachineResponsible>()
                .HasKey(x => new { x.MachineID, x.ResponsibleID });

            modelBuilder.Entity<MachineDevice>()
                .HasKey(x => new { x.MachineID, x.DeviceID });

        }

        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceDocument> DeviceDocument { get; set; }
        public DbSet<MachineComment> MachineComments { get; set; }
        public DbSet<Responsible> Responsible { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Machine> Machine { get; set; }
    }
}

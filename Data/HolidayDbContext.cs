using HolidayAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace HolidayAssessment.Data
{
    public class HolidayDbContext(DbContextOptions<HolidayDbContext> options) : DbContext(options)
    {
        public DbSet<Holiday> Holidays => Set<Holiday>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.Property(x => x.CountryCode)
                    .HasMaxLength(2);

                entity.Property(x => x.LocalName)
                    .HasMaxLength(100);

                entity.Property(x => x.Name)
                    .HasMaxLength(200);

                entity.Property(x => x.Types)
                    .HasMaxLength(200);

                entity.Property(x => x.Counties)
                    .HasMaxLength(500);

                entity.HasIndex(x => new { x.CountryCode, x.Date })
                    .IsUnique();
            });
        }
    }
}

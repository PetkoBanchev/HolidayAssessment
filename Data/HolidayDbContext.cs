using HolidayAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace HolidayAssessment.Data
{
    public class HolidayDbContext(DbContextOptions<HolidayDbContext> options) : DbContext(options)
    {
        public DbSet<Holiday> Holidays => Set<Holiday>();
        public DbSet<Country> Countries => Set<Country>();

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

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(x => x.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(x => x.CountryCode)
                    .IsUnique();
            });
        }
    }
}

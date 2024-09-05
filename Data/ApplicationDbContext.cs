using Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<Condition> Conditions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Current>()
                .HasOne(c => c.Condition)
                .WithMany()
                .HasForeignKey("ConditionId");

            base.OnModelCreating(modelBuilder);
        }
    }
}
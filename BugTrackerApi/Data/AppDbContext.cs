using BugTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Project> Projects=>Set<Project>();
        public DbSet<Issue> Issues=>Set<Issue>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(e =>
            {
                e.Property(p => p.Name).IsRequired().HasMaxLength(200);
                e.Property(p => p.Description).HasMaxLength(200);
            });
            modelBuilder.Entity<Issue>(e =>
            {
                e.Property(i => i.Title).IsRequired().HasMaxLength(200);
                e.Property(i => i.Status).HasConversion<int>();
                e.HasOne(i => i.Project).WithMany(p => p.Issues).HasForeignKey(i => i.ProjectId).OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}

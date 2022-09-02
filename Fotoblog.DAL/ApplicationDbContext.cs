using Fotoblog.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fotoblog.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TagEntity> TagEntities { get; set; }
        public DbSet<PhotoEntity> PhotoEntities { get; set; }
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoEntity>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Photos)
                .UsingEntity(j => j.ToTable("PhotosTags"));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

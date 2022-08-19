using Fotoblog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

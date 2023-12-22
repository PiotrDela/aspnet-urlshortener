using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure
{
    public class SqlServerContext : DbContext
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }


        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity(NumberSequence.SequenceName).Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity(NumberSequence.SequenceName).HasKey("Id");

            modelBuilder.Entity<ShortUrl>().Property(x => x.OriginalUrl).IsRequired();
            modelBuilder.Entity<ShortUrl>().Property(x => x.Slug).IsRequired();

            modelBuilder.Entity<ShortUrl>().HasKey(x => x.OriginalUrl);
            modelBuilder.Entity<ShortUrl>().HasIndex(x => x.Slug).IsUnique();
        }
    }
}

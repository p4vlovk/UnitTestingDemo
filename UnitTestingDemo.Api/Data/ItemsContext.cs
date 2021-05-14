namespace UnitTestingDemo.Api.Data
{
    using Microsoft.EntityFrameworkCore;

    using UnitTestingDemo.Api.Models;

    public class ItemsContext : DbContext
    {
        public ItemsContext(DbContextOptions<ItemsContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(
                b =>
                {
                    b.Property("id");
                    b.HasKey("id");
                    b.Property(e => e.Name);
                    b.HasMany(e => e.Tags).WithOne().IsRequired();
                });

            modelBuilder.Entity<Tag>(
                b =>
                {
                    b.Property("id");
                    b.HasKey("id");
                    b.Property(e => e.Label);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}

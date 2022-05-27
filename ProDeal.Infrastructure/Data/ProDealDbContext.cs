using Microsoft.EntityFrameworkCore;
using ProDealChallenge.Domain.Models;

namespace ProDeal.Infrastructure.Data
{
    public  class ProDealDbContext : DbContext
    {
        public DbSet<FolderItem> FolderItems { get; set; }

        public ProDealDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FolderItem>()
                .HasKey(x => x.Id)
                .HasName("PrimaryKey_id");

            builder.Entity<FolderItem>()
                .HasIndex(x => x.ExternalId)
                .HasDatabaseName("Index_external_id")
                .IsUnique();

            builder.Entity<FolderItem>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasPrincipalKey(x => x.ExternalId)
                .HasForeignKey(x => x.ParentExternalId);

            builder.Entity<FolderItem>().Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Entity<FolderItem>().Property(x => x.ExternalId)
                .IsRequired()
                .HasColumnName("external_id");

            builder.Entity<FolderItem>().Property(x => x.ParentExternalId)
                .HasColumnName("parent_external_id");

            builder.Entity<FolderItem>().Property(x => x.ItemName)
                .IsRequired()
                .HasColumnName("item_name");

            builder.Entity<FolderItem>().Property(x => x.Priority)
                .IsRequired()
                .HasColumnName("priority");

            base.OnModelCreating(builder);
        }
    }
}

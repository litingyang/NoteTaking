using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApplication1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Note> Note { get; set; } = default!;
        public DbSet<IdentityUser> User { get; set; } = default!;
        //public DbSet<CusUser> ExternalUser { get; set; } = default!;
        public DbSet<IdentityUserLogin<string>> UserLogin { get; set; } = default!;
        public DbSet<IdentityRole> Role { get; set; } = default!;

        public DbSet<IdentityUserClaim<string>> UserClaim { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");
            });
            //    builder.Entity<IdentityUser>(b =>
            //    {
            //        // Primary key
            //        b.HasKey(u => u.Id);

            //        // Indexes for "normalized" username and email, to allow efficient lookups
            //        b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            //        b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            //        // Maps to the AspNetUsers table
            //        b.ToTable("AspNetUsers");

            //        // A concurrency token for use with the optimistic concurrency checking
            //        b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            //        // Limit the size of columns to use efficient database types
            //        b.Property(u => u.UserName).HasMaxLength(256);
            //        b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            //        b.Property(u => u.Email).HasMaxLength(256);
            //        b.Property(u => u.NormalizedEmail).HasMaxLength(256);
            //        b.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            //        // The relationships between User and other entity types
            //        // Note that these relationships are configured with no navigation properties
            //    });
            //    builder.Entity<IdentityUserLogin<string>>(b =>
            //    {
            //        b.ToTable("MyUserLogins");
            //    });
            //    // Customize the ASP.NET Identity model and override the defaults if needed.
            //    // For example, you can rename the ASP.NET Identity table names and more.
            //    // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
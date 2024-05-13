using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prism.Domain.Entities;

namespace Prism.DataAccess.Data
{
    public class PrismDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public PrismDbContext(DbContextOptions<PrismDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRole");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            // Định nghĩa các quan hệ giữa các bảng
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

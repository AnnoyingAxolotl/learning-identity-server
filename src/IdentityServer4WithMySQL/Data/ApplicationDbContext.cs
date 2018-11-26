using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4WithMySQL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Shorten key length for Identity
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(127);
                entity.Property(m => m.EmailConfirmed).HasColumnType("bit(1)");
                entity.Property(m => m.LockoutEnabled).HasColumnType("bit(1)");
                entity.Property(m => m.PhoneNumberConfirmed).HasColumnType("bit(1)");
                entity.Property(m => m.TwoFactorEnabled).HasColumnType("bit(1)");
            });

            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(127));
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.ProviderKey).HasMaxLength(127);
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.RoleId).HasMaxLength(127);
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.Name).HasMaxLength(127);
            });

            base.OnModelCreating(builder);
        }
    }
}

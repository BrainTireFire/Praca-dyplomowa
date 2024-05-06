using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models;
using pracadyplomowa.Models.Entities;

namespace pracadyplomowa;

public class AppIdentityDbContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
                base.OnModelCreating(builder);

                builder.Entity<User>()
                        .HasMany(ur => ur.UserRoles)
                        .WithOne(u => u.User)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();

                builder.Entity<Role>()
                        .HasMany(ur => ur.UserRoles)
                        .WithOne(u => u.Role)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                        
                builder.Entity<ObjectWithOwner>().UseTptMappingStrategy();
                builder.Entity<ObjectWithOwner>()
                        .HasKey(i => i.Id);
                builder.Entity<ObjectWithOwner>()
                        .HasOne(i => i.Owner)
                        .WithMany(o => o.Objects)
                        .HasForeignKey(i => i.OwnerId)
                        .IsRequired();
                
                        
                builder.Entity<Item>();

        }

        public DbSet<Item> Items {get; set;}
        public DbSet<ObjectWithOwner> Objects {get; set;}

}
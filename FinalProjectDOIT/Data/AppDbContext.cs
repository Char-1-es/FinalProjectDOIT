using FinalProjectDOIT.Data;
using FinalProjectDOIT.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectDOIT.Data
{
        public class AppDbContext : IdentityDbContext<User>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.SeedUsers();
                modelBuilder.SeedRoles();
                modelBuilder.SeedUserRoles();

                modelBuilder.Entity<Comment>()
                    .HasOne(c => c.Topic)
                    .WithMany(t => t.Comments)
                    .HasForeignKey(c => c.TopicId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Comment>()
                    .HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            }

            public DbSet<User> ForumUsers { get; set; }
            public DbSet<Topic> Topics { get; set; }
            public DbSet<Comment> Comments { get; set; }

        }
    }

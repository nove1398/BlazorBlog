using BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Server.Data
{
    public class BlazorBlogContext : DbContext
    {

        public BlazorBlogContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            List<User> users = new List<User>()
            {
                new User(){UserId = 1, FirstName = "nove", LastName = "drew", Username ="TempName", Email ="drew@yahoo.com", Status = User.AccountStatus.Active, 
                            Birthday = DateTime.Parse("11/28/1989"), Avatar="", Password="password", Salt="salty"},
                new User(){UserId = 2, FirstName = "2", LastName = "2", Username ="TempName2", Email ="drew2@yahoo.com", Status = User.AccountStatus.Pending,
                            Birthday = DateTime.Parse("11/28/1989"), Avatar="", Password="password", Salt="salty"}
            };
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Salt).HasMaxLength(250).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(250).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.Password).HasMaxLength(250).IsRequired();
                entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(u => u.Avatar).HasMaxLength(250).IsRequired(false);
                entity.Property(u => u.Username).HasMaxLength(20).IsRequired(false);
                entity.Property(u => u.Birthday).HasMaxLength(50).IsRequired(false);
                entity.Property(u => u.Status).HasMaxLength(50).IsRequired().HasConversion<string>(); ;
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(u => u.ReadAt).IsRequired(false);
                entity.Property(m => m.IsArchived).HasDefaultValue(false);
                entity.Property(m => m.Body).IsRequired();
                entity.Property(m => m.Subject).HasMaxLength(200).IsRequired(false);
                entity.Property(u => u.SentAt).IsRequired().HasDefaultValueSql("GETDATE()");
                entity.HasOne(m => m.UserTo).WithMany(u => u.Messages).HasForeignKey(m => m.UserToId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(m => m.UserFrom).WithMany(u => u.Messages).HasForeignKey(m => m.UserFromId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                entity.HasOne(ur => ur.Role).WithMany(u => u.UserRoles).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(r => r.Name).HasMaxLength(15).IsRequired();
                entity.Property(r => r.Description).HasMaxLength(500).IsRequired();
                
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}

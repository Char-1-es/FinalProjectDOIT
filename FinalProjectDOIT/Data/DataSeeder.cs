using FinalProjectDOIT.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FinalProjectDOIT.Data
{
    public static class DataSeeder
    {
        public static void SeedUsers(this ModelBuilder builder)
        {
            PasswordHasher<User> hasher = new();

            builder.Entity<User>().HasData(
                    new User()
                    {
                        Id = "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                        UserName = "admin@gmail.com",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = hasher.HashPassword(null, "Admin123!"),
                        PhoneNumber = "555337681",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    },
                    new User()
                    {
                        Id = "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                        UserName = "nika@gmail.com",
                        NormalizedUserName = "NIKA@GMAIL.COM",
                        Email = "nika@gmail.com",
                        NormalizedEmail = "NIKA@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = hasher.HashPassword(null, "Nika123!"),
                        PhoneNumber = "558490645",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    },
                    new User()
                    {
                        Id = "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                        UserName = "gio@gmail.com",
                        NormalizedUserName = "GIO@GMAIL.COM",
                        Email = "gio@gmail.com",
                        NormalizedEmail = "GIO@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = hasher.HashPassword(null, "Gio123!"),
                        PhoneNumber = "551442269",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    }
                );
        }

        public static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "33B7ED72-9434-434A-82D4-3018B018CB87", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", Name = "User", NormalizedName = "USER" }
            );

        }

        public static void SeedUserRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "33B7ED72-9434-434A-82D4-3018B018CB87", UserId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031" },
                new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "D514EDC9-94BB-416F-AF9D-7C13669689C9" },
                new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "87746F88-DC38-4756-924A-B95CFF3A1D8A" }
            );
        }

    }
}
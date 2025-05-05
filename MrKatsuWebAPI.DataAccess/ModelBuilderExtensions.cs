using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MrKatsuWebAPI.DataAccess.Entities;

namespace MrKatsuWebAPI.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Admintrator",
                },
                new AppRole
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "Admintrator",
                }
            );
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@mail.com",
                    Avatar = "assets/images/avatars/564.png",
                    NormalizedEmail = "ADMIN@EMAIL.COM",
                    IsDeleted = false,
                    TimeCreated = DateTime.Now,
                    TimeUpdated = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),                   
                },
                new AppUser
                {
                    Id = 2,
                    UserName = "hunghero",
                    NormalizedUserName = "HUNGHERO",
                    Email = "hunghero@mail.com",
                    NormalizedEmail = "HUNGHERO@MAIL.COM",
                    Avatar = "assets/images/avatars/vip/1475.png",
                    IsDeleted = false,
                    TimeCreated = DateTime.Now,
                    TimeUpdated = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                },
                new AppUser
                {
                    Id = 3,
                    UserName = "katsu",
                    NormalizedUserName = "KATSU",
                    Email = "katsu@mail.com",
                    NormalizedEmail = "KATSU@MAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    Avatar = "assets/images/avatars/vip/1360.png",
                    IsDeleted = false,
                    TimeCreated = DateTime.Now,
                    TimeUpdated = DateTime.Now,
                },
                new AppUser
                {
                    Id = 4,
                    UserName = "gatapchoi",
                    NormalizedUserName = "GATAPCHOI",
                    Email = "gatapchoi@email.com",
                    NormalizedEmail = "GATAPCHOI@EMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    Avatar = "assets/images/avatars/vip/1361.png",
                    IsDeleted = false,
                    TimeCreated = DateTime.Now,
                    TimeUpdated = DateTime.Now,
                },
                new AppUser
                {
                    Id = 5,
                    UserName = "manhhdc",
                    NormalizedUserName = "MANHHDC",
                    Email = "manhhdc@email.com",
                    NormalizedEmail = "MANHHDC@EMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    Avatar = "assets/images/avatars/vip/1474.png",
                    IsDeleted = false,
                    TimeCreated = DateTime.Now,
                    TimeUpdated = DateTime.Now
                },
                new AppUser
                {
                    Id = 6,
                    UserName = "ehvn",
                    NormalizedUserName = "EHVN",
                    Email = "ehvn@email.com",
                    NormalizedEmail = "ehvn@email.com",
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    Avatar = "assets/images/avatars/1209.png",
                    IsDeleted = false,
                    TimeCreated = DateTime.Now,
                    TimeUpdated = DateTime.Now
                },
                 new AppUser
                 {
                     Id = 13,
                     UserName = "tanhieuno4",
                     NormalizedUserName = "TANHIEUNO4",
                     Email = "tanhieuno4@mail.com",
                     Avatar = "assets/images/avatars/vip/1360.png",
                     NormalizedEmail = "TANHIEUNO4@EMAIL.COM",
                     IsDeleted = false,
                     TimeCreated = DateTime.Now,
                     TimeUpdated = DateTime.Now,
                     PasswordHash = hasher.HashPassword(null, "Abc@123"),
                 }
            );
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                },
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 2
                },
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 3
                },
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 4
                },
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 5
                },
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 6
                }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "android",
                    Description = "Chi tiết danh mục"
                },
                new Category
                {
                    Id = 2,
                    Name = "pc",
                    Description = "Chi tiết danh mục"
                },
                new Category
                {
                    Id = 3,
                    Name = "java",
                    Description = "Chi tiết danh mục"
                },
                new Category
                {
                    Id = 4,
                    Name = "ios",
                    Description = "Chi tiết danh mục"
                }
            );
        }
    }
}

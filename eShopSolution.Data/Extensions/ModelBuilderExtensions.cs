using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
                new AppConfig() { Key = "HomeKeyWord", Value = "This is keyword of eShopSolution" },
                new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en-US", Name = "English", IsDefault = false });


            modelBuilder.Entity<Category>().HasData(
                new Category() {
                    Id = 1,
                    IsShowOnHome = true, 
                    ParentId = null, 
                    SortOrder = 1,
                    Status = Enums.Status.Active,
                    //CategoryTranslations = new List<CategoryTranslation>() { 
                    //    new CategoryTranslation() { Name = "Áo nam", LanguageId = "vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Thời trang nam"}, 
                    //    new CategoryTranslation() { Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "men-shirt", SeoDescription = "The shirt product for nam", SeoTitle = "The shirt men"} 
                    //}
                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Enums.Status.Active,
                    //CategoryTranslations = new List<CategoryTranslation>() {
                    //    new CategoryTranslation() { Name = "Áo nữ", LanguageId = "vi-VN", SeoAlias = "ao-nu", SeoDescription = "Sản phẩm áo thời trang nữ", SeoTitle = "Thời trang nữ"},
                    //    new CategoryTranslation() { Name = "Women Shirt", LanguageId = "en-US", SeoAlias = "women-shirt", SeoDescription = "The shirt product for women", SeoTitle = "The shirt women"}
                    //}
                });

            modelBuilder.Entity<CategoryTranslation>().HasData(
                    new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Áo nam", LanguageId = "vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Thời trang nam" },
                    new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "men-shirt", SeoDescription = "The shirt product for nam", SeoTitle = "The shirt men" },
                    new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Áo nữ", LanguageId = "vi-VN", SeoAlias = "ao-nu", SeoDescription = "Sản phẩm áo thời trang nữ", SeoTitle = "Thời trang nữ" },
                    new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Women Shirt", LanguageId = "en-US", SeoAlias = "women-shirt", SeoDescription = "The shirt product for women", SeoTitle = "The shirt women" }

                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id=1, DateCreated = DateTime.Now, OriginalPrice = 100000,Price = 200000, Stock = 0, ViewCount = 0, 
                    
                    
                });
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Áo nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Thời trang nam",
                    Details = "Mô tả sản phẩm",
                    Description = ""
                },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt product for men",
                    SeoTitle = "The shirt men",
                    Details = "Description of product",
                    Description = ""
                });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 });

            // any guid
            var roleId = new Guid("a18be9c0-aa65-4af8-bd17-00bd9344e575");
            var adminId = new Guid("D1F771DA-B318-42F8-A003-5A15614216F5");
            
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator Role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "onionwebdev@gmail.com",
                NormalizedEmail = "onionwebdev@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "toiyeuemnhat"),
                SecurityStamp = string.Empty,
                FirstName = "Nhat",
                LastName = "Luong Xuan",
                Dob = new DateTime(2023,01,19)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}

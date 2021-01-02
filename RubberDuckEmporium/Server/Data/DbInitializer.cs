using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubberDuckEmporium.Shared;

namespace RubberDuckEmporium.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context, UserManager<IdentityUser<Guid>> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            context.Database.EnsureCreated();

            if(context.Roles.Any())
            {
                return;
            }
            Guid adminGuid = new("cbe4d72d-f3f4-426c-8b59-283bb4d3f2a8");
            Guid defaultGuid = new("2becafb8-9989-4643-b7be-bc6876269444");

            var adminUser = new IdentityUser<Guid> { Id = adminGuid, UserName = "Admin" };
            var defaultUser = new IdentityUser<Guid> { Id = defaultGuid, UserName = "Default" };

            context.Products.Add(new ProductModel { ProductID = 1, Name = "A Veritable Assortment", Description = "They are all very different but their bond is strong.", Price = 4000, ImageURL = "images/1nGxy0Mh8eA-unsplash.jpg" }); 
            context.Products.Add(new ProductModel { ProductID = 2, Name = "Duck Apocalypse", Description = "They were billions.", Price = 5000, ImageURL = "images/59yg_LpcvzQ-unsplash.jpg" }); 
            context.Products.Add(new ProductModel { ProductID = 3, Name = "Waterpark Wanderers", Description = "Escapees from the jacuzzi.", Price = 3000, ImageURL = "images/pexels-106144.jpg" });
            context.Products.Add(new ProductModel { ProductID = 4, Name = "BatDuck", Description = "A duck who tragically lost its parents at a young age.", Price = 600, ImageURL = "images/wF7GqWA3Tag-unsplash.jpg" });
            context.Products.Add(new ProductModel { ProductID = 5, Name = "The Duckvengers", Description = "The bath's greatest heroes.", Price = 3000, ImageURL = "images/pexels-122308.jpg" });
            context.Products.Add(new ProductModel { ProductID = 6, Name = "Bob and his Sickly Brother", Description = "Bob's bro had some dodgy sushi for dinner.", Price = 1200, ImageURL = "images/pexels-132464.jpg" });
            context.Products.Add(new ProductModel { ProductID = 7, Name = "Gargantuan Titan Duck", Description = "A ducky belonging to Poseidon himself.", Price = 50000, ImageURL = "images/nqlMPecTWQ-unsplash.jpg" });
            context.Products.Add(new ProductModel { ProductID = 8, Name = "Mysterious Feathered Rubber Duck", Description = "This strange rubber duck even moves of its own accord.", Price = 10000, ImageURL = "images/Qf3j1lHfyik-unsplash.jpg" });
            context.Products.Add(new ProductModel { ProductID = 9, Name = "Unicorn Duck", Description = "An extremely rare duck revered by collectors.", Price = 3200, ImageURL = "images/pexels-592677.jpg" });

            context.SaveChanges();

            var userRole = new IdentityRole<Guid> { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString() };
            var adminRole = new IdentityRole<Guid> { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid(), ConcurrencyStamp = Guid.NewGuid().ToString() };

            roleManager.CreateAsync(userRole).GetAwaiter().GetResult();
            roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();

            userManager.CreateAsync(adminUser, "P@ssword1#").GetAwaiter().GetResult();
            userManager.CreateAsync(defaultUser, "P@ssword1#").GetAwaiter().GetResult();

            userManager.AddToRoleAsync(defaultUser, "User").GetAwaiter().GetResult();
            userManager.AddToRolesAsync(adminUser, new string[] { "User", "Admin" }).GetAwaiter().GetResult();
        }
    }
}
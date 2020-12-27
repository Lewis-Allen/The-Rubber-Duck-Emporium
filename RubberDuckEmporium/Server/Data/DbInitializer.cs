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

            Guid adminGuid = Guid.NewGuid();
            Guid defaultGuid = Guid.NewGuid();

            var adminUser = new IdentityUser<Guid> { Id = adminGuid, UserName = "Admin" };
            var defaultUser = new IdentityUser<Guid> { Id = defaultGuid, UserName = "Default" };

            context.Products.Add(new ProductModel { ProductID = 1, Name = "Batman Rubber Duck", Price = 600 }); 
            context.Products.Add(new ProductModel { ProductID = 2, Name = "Superman Rubber Duck", Price = 600 }); 
            context.Products.Add(new ProductModel { ProductID = 3, Name = "Spiderman Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ProductID = 4, Name = "Captain Marvel Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ProductID = 5, Name = "Thanos Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ProductID = 6, Name = "Captain America Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ProductID = 7, Name = "Hulk Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ProductID = 8, Name = "Avengers Rubber Duck Set", Price = 2000 });
            context.Products.Add(new ProductModel { ProductID = 9, Name = "Justice League Rubber Duck Set", Price = 2000 });

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
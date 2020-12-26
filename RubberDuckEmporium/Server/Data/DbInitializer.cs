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
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if(context.Roles.Any())
            {
                return;
            }

            context.Roles.Add(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            context.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });

            context.Products.Add(new ProductModel { ID = 1, Name = "Batman Rubber Duck", Price = 600 }); 
            context.Products.Add(new ProductModel { ID = 2, Name = "Superman Rubber Duck", Price = 600 }); 
            context.Products.Add(new ProductModel { ID = 3, Name = "Spiderman Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ID = 4, Name = "Captain Marvel Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ID = 5, Name = "Thanos Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ID = 6, Name = "Captain America Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ID = 7, Name = "Hulk Rubber Duck", Price = 600 });
            context.Products.Add(new ProductModel { ID = 8, Name = "Avengers Rubber Duck Set", Price = 2000 });
            context.Products.Add(new ProductModel { ID = 9, Name = "Justice League Rubber Duck Set", Price = 2000 });

            context.SaveChanges();
        }
    }
}

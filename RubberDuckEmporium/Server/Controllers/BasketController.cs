using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RubberDuckEmporium.Server.Data;
using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public BasketController(AppDbContext context, UserManager<IdentityUser<Guid>> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<BasketModel> Get()
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var basket = _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefault(b => b.UserID == currentUser.Id);

            if(basket is null)
            {
                basket = new();
                basket.UserID = currentUser.Id;
                basket.BasketItems = new List<BasketItemModel>();

                _context.Baskets.Add(basket);
                await _context.SaveChangesAsync();
            }

            return basket;
        }

        [HttpPost]
        [Route("")]
        public async Task<BasketModel> AddProduct(ProductModel product, int quantity)
        {
            var basket = await Get();

            var existing = basket.BasketItems?.Find(p => p.Product?.ProductID == product.ProductID);

            if(existing is null)
            {
                BasketItemModel newItem = new();

                var productItem = _context.Products.Find(product.ProductID);
                newItem.Product = productItem;
                newItem.Quantity = quantity;

                if (basket.BasketItems is null)
                    basket.BasketItems = new();

                basket.BasketItems.Add(newItem);
            }
            else
            {
                existing.Quantity += quantity;
            }
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();

            return basket;
        }

        [HttpPut]
        [Route("")]
        public async Task<BasketModel> UpdateProduct(ProductModel product, int quantity)
        {
            var basket = await Get();

            var existing = basket.BasketItems.Find(p => p.Product.ProductID == product.ProductID);

            existing.Quantity = quantity;

            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();

            return basket;
        }

        [HttpDelete]
        [Route("")]
        public async Task<BasketModel> ClearBasket()
        {
            var basket = await Get();

            if(basket == null)
            {
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                basket = new();
                basket.UserID = user.Id;
                _context.Baskets.Add(basket);
            }
            else
            {
                basket.BasketItems = new();
                _context.Baskets.Update(basket);
            }

            await _context.SaveChangesAsync();

            return basket;
        }
    }
}

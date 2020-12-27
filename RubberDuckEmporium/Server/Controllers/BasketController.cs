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
                .ThenInclude(bi => bi.Product)
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
        public async Task<BasketModel> AddProduct([FromBody]BasketItemModel basketItem)
        {
            var basket = await Get();

            var existing = basket.BasketItems?.Find(p => p.Product?.ProductID == basketItem.Product.ProductID);

            if(existing is null)
            {
                BasketItemModel newItem = new();

                var productItem = _context.Products.Find(basketItem.Product.ProductID);
                newItem.Product = productItem;
                newItem.Quantity = basketItem.Quantity;

                basket.BasketItems.Add(newItem);
            }
            else
            {
                existing.Quantity += basketItem.Quantity;
            }
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();

            return basket;
        }

        [HttpPut]
        [Route("")]
        public async Task<BasketModel> UpdateProduct([FromBody]BasketItemModel basketItem)
        {
            var basket = await Get();

            if(basketItem.Quantity == 0)
            {
                basket.BasketItems.RemoveAll(bi => bi.Product.ProductID == basketItem.Product.ProductID);
            }
            else
            {
                var existing = basket.BasketItems.Find(p => p.Product.ProductID == basketItem.Product.ProductID);
                existing.Quantity = basketItem.Quantity;
            }

            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();

            return basket;
        }

        [HttpDelete]
        [Route("")]
        public async Task<BasketModel> ClearBasket()
        {
            var basket = await Get();

            basket.BasketItems = new();
            _context.Baskets.Update(basket);

            await _context.SaveChangesAsync();

            return basket;
        }
    }
}

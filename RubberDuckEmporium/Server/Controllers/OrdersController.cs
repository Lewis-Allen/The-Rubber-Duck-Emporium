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
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public OrdersController(AppDbContext context, UserManager<IdentityUser<Guid>> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("")]
        public async Task<OrderModel> PlaceOrder(BasketModel basket)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            OrderModel order = new();
            order.Status = OrderStatus.ORDERPLACED;
            order.OrderItems = basket.BasketItems.Select(bi => new OrderItemModel(_context.Products.Find(bi.Product.ProductID), bi.Quantity)).ToList();
            order.UserID = currentUser.Id;

            _context.Orders.Add(order);

            var userBasket = _context.Baskets
                .Include(b => b.BasketItems)
                .First(b => b.UserID == currentUser.Id);

            userBasket.BasketItems = new();

            _context.Baskets.Update(userBasket);
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpGet]
        [Route("{orderID:int}")]
        public async Task<OrderModel> Retrieve(int orderID)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderID == orderID && o.UserID == currentUser.Id);

            return order;
        }

        [HttpGet]
        [Route("")]
        public async Task<List<OrderModel>> RetrieveAllForUser()
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserID == currentUser.Id)
                .ToList();

            return orders;
        }
    }
}

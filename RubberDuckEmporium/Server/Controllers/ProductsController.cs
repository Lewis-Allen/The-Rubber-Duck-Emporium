using Microsoft.AspNetCore.Mvc;
using RubberDuckEmporium.Server.Data;
using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetAll()
        {
            return _context.Products.ToArray();
        }
    }
}

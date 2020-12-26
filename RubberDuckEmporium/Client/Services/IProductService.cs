using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Client.Services
{
    public interface IProductService
    {
        public Task<ProductModel[]> GetAllProducts();
    }
}

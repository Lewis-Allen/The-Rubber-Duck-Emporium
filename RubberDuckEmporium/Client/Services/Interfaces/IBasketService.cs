using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Client.Services.Interfaces
{
    public interface IBasketService
    {
        public Task<BasketModel> GetBasket();
        public Task<BasketModel> AddToBasket(ProductModel product, int quantity);
        public Task<BasketModel> ClearBasket();
    }
}

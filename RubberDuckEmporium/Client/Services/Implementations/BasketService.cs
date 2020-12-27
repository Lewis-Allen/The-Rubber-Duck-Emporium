using RubberDuckEmporium.Client.Services.Interfaces;
using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Client.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BasketModel> AddToBasket(ProductModel product, int quantity)
        {
            var res = await _httpClient.PostAsJsonAsync("/api/basket", new { product, quantity });
            var basket = await res.Content.ReadFromJsonAsync<BasketModel>();
            return null;
        }

        public void RemoveFromBasket(ProductModel product, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<BasketModel> ClearBasket()
        {
            throw new NotImplementedException();
        }

        public Task<BasketModel> GetBasket()
        {
            var basket = _httpClient.GetFromJsonAsync<BasketModel>("/api/basket");

            return basket;
        }
    }
}

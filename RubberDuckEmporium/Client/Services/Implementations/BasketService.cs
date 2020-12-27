using RubberDuckEmporium.Client.Services.Interfaces;
using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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

        public async Task<BasketModel> AddToBasket(BasketItemModel basketItem)
        {
            var res = await _httpClient.PostAsJsonAsync("/api/basket", basketItem);

            var basket = await res.Content.ReadFromJsonAsync<BasketModel>();
            return basket;
        }

        public async Task<BasketModel> UpdateBasketItem(BasketItemModel basketItem)
        {
            var res = await _httpClient.PutAsJsonAsync("/api/basket", basketItem);

            var basket = await res.Content.ReadFromJsonAsync<BasketModel>();
            return basket;
        }

        public async Task<BasketModel> ClearBasket()
        {
            var res = await _httpClient.DeleteAsync("/api/basket");

            var basket = await res.Content.ReadFromJsonAsync<BasketModel>();
            return basket;
        }

        public Task<BasketModel> GetBasket()
        {
            var basket = _httpClient.GetFromJsonAsync<BasketModel>("/api/basket");

            return basket;
        }
    }
}

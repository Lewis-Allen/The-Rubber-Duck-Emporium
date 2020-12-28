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
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderModel> PlaceOrder(BasketModel basket)
        {
            var res = await _httpClient.PostAsJsonAsync("/api/orders", basket);

            var order = await res.Content.ReadFromJsonAsync<OrderModel>();
            return order;
        }

        public async Task<List<OrderModel>> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderModel>> RetrieveAllForUser()
        {
            var res = await _httpClient.GetFromJsonAsync<List<OrderModel>>("/api/orders");

            return res;
        }

        public async Task<OrderModel> Retrieve(int orderID)
        {
            var res = await _httpClient.GetFromJsonAsync<OrderModel>($"/api/orders/{orderID}");

            return res;
        }
    }
}

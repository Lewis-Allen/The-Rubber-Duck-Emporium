using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Client.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<OrderModel>> RetrieveAllForUser();
        public Task<OrderModel> PlaceOrder(BasketModel basket);
        public Task<OrderModel> Retrieve(Guid orderID);
    }
}

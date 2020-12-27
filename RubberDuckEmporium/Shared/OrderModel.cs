using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Shared
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}

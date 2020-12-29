using RubberDuckEmporium.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Shared
{
    public class OrderModel
    {
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }

        public static OrderStatus GetStatus(OrderModel order)
        {
            var secondsSinceOrder = (DateTime.Now - order.CreatedDate).TotalSeconds;
            
            OrderStatus status = secondsSinceOrder switch
            {
                > 120 => OrderStatus.DELIVERED,
                > 90 => OrderStatus.OUTFORDELIVERY,
                > 60 => OrderStatus.DISPATCHED,
                > 30 => OrderStatus.PREPARINGFORDISPATCH,
                _ => OrderStatus.ORDERPLACED,
            };

            return status;
        }
    }
}

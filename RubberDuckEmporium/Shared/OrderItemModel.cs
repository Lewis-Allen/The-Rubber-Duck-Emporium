using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Shared
{
    public class OrderItemModel
    {
        public int ProductID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

        public int OrderID { get; set; }
        public OrderModel Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Shared
{
    public class OrderItemModel
    {
        public OrderItemModel() { }

        public OrderItemModel(ProductModel product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public int OrderItemID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}

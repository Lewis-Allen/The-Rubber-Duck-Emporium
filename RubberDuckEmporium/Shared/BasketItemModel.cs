using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Shared
{
    public class BasketItemModel
    {
        public BasketItemModel() { }

        public BasketItemModel(ProductModel product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public int BasketItemID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}

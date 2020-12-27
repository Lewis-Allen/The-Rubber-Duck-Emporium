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

        public BasketItemModel(BasketModel basket, ProductModel product, int quantity)
        {
            Basket = basket;
            Product = product;
            Quantity = quantity;
        }

        public int BasketItemID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

        public BasketModel Basket { get; set; }
    }
}

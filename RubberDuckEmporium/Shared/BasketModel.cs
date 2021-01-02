using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Shared
{
    public class BasketModel
    {
        public Guid BasketID { get; set; }
        public Guid UserID { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
    }
}

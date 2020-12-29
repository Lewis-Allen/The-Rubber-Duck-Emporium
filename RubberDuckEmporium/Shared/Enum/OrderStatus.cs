using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Shared.Enum
{
    public enum OrderStatus
    {
        OrderPlaced,
        PreparingForDispatch,
        Dispatched,
        OutForDelivery,
        Delivered
    }
}

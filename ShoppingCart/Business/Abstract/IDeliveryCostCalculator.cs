using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Abstract
{
    public interface IDeliveryCostCalculator
    {
        double CalculateFor(ShoppingCart shoppingCart);
    }
}

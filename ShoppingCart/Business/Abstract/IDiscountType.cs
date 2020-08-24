using ShoppingCart.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Abstract
{
    public interface IDiscountType
    {
        double CalculateDiscountForType(Campaign campaign, double totalPriceByCategory);
    }
}

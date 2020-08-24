using ShoppingCart.Business.Abstract;
using ShoppingCart.Domain;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ShoppingCart.Business.Concrete
{
    public class DiscountTypeAmount : IDiscountType
    {
        public double CalculateDiscountForType(Campaign campaign, double totalPriceByCategory)
        {
            if (campaign == null) return 0;

            return campaign.DiscountAmount;
        }
    }
}

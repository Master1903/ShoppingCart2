using ShoppingCart.Business.Abstract;
using ShoppingCart.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain
{
    public class Campaign
    {
        public Category Category { get; set; }
        public double DiscountAmount { get; set; }
        public int MinProductQuantity { get; set; }
        public IDiscountType DiscountType { get; set; }

        public Campaign(Category category, double discountAmount, int minProductQuantity, IDiscountType discountType)
        {
            Category = category;
            DiscountAmount = discountAmount;
            MinProductQuantity = minProductQuantity;
            DiscountType = discountType;
        }

        public double CalculateCampaignDiscount(Campaign campaign, double totalPriceByCategory)
        {
            return DiscountType.CalculateDiscountForType(campaign, totalPriceByCategory);
        }


    }
}

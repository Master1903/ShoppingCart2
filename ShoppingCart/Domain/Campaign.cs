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
        public DiscountType DiscountType { get; set; }


        public Campaign(Category category, double discountAmount, int minProductQuantity, DiscountType discountType)
        {
            Category = category;
            DiscountAmount = discountAmount;
            MinProductQuantity = minProductQuantity;
            DiscountType = discountType;
        }
    }
}

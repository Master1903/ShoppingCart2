using ShoppingCart.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain
{
    public class Coupon
    {
        public int DiscountAmount { get; set; }
        public double MinimumCartAmount { get; set; }
        public DiscountType DiscountType { get; set; }

        public Coupon(double minimumCartAmount, int discountAmount,DiscountType discountType)
        {
            MinimumCartAmount = minimumCartAmount;
            DiscountAmount = discountAmount;
            DiscountType = DiscountType;
        }

    }
}

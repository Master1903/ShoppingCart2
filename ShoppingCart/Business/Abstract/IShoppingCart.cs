using ShoppingCart.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Abstract
{
    public interface IShoppingCart
    {
        double GetTotalAmountAfterDiscounts();
        double GetCampaignDiscount();
        double GetCouponDiscount();
        double GetDeliveryCost();
        int GetNumberOfProducts();
        int GetNumberOfDeliveries();
        string Print();
        void AddItem(Product product, int quantity);
        void ApplyDiscounts(params Campaign[] discounts);
        void ApplyCoupon(Coupon coupon);
        IDictionary<string, Product> Products { get; set; }
    }
}

using ShoppingCart.Domain;
using ShoppingCart.Domain.Enums;
using System;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {

            ShoppingCart cart = new ShoppingCart(new DeliveryCostCalculate(5, 2, 2.99));

            //Add Category
            Category categoryFood = new Category("Food");
            Category categorySport = new Category("Sport");
            Category categoryBasketball = new Category("Basketball Boots");

            //Food Product
            Product banana = new Product("Banana", 20, categoryFood);
            Product apricot = new Product("Apricot", 10, categoryFood);

            //Sport Product
            Product skateboard = new Product("skateboard", 30, categorySport);
            Product ball = new Product("ball", 20, categorySport);


            //add product
            cart.AddItem(ball, 3);
            cart.AddItem(skateboard, 3);
            cart.AddItem(banana, 2);
            cart.AddItem(apricot, 2);

            //campanign
            Campaign campaign1 = new Campaign(categorySport, 20, 2, DiscountType.Rate);
            Campaign campaign2 = new Campaign(categorySport, 25, 2, DiscountType.Amount);
            Campaign campaign3 = new Campaign(categoryFood, 50, 2, DiscountType.Rate);

            cart.ApplyDiscounts(campaign1, campaign2, campaign3);

            //coupon
            var coupon = new Coupon(50, 10, DiscountType.Rate);
            cart.ApplyCoupon(coupon);

            double totalAmountAfterDiscount = cart.GetTotalAmountAfterDiscounts();
            double campaignDiscount = cart.GetCampaignDiscount();
            double deliveryCost = cart.GetDeliveryCost();
            double couponDiscount = cart.GetCouponDiscount();

            Console.WriteLine(cart.Print());
            Console.ReadLine();
        }
    }
}

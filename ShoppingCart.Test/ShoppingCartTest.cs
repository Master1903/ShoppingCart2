using Moq;
using ShoppingCart.Business.Abstract;
using ShoppingCart.Business.Concrete;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Enums;
using System;
using Xunit;

namespace ShoppingCart.Test
{
    public class ShoppingCartTest
    {
        private IShoppingCart _shoppingCart;
        private Mock<IDeliveryCostCalculator> _deliveryCostCalculator;

        Category fashion, food;
        Product jean, hat, orange, apple;
        Campaign campaign1, campaign2, campaign3;
        Coupon coupon;


        public ShoppingCartTest()
        {
            _deliveryCostCalculator = new Mock<IDeliveryCostCalculator>();
            _shoppingCart = new ShoppingCart(_deliveryCostCalculator.Object);

            //category fashion
            fashion = new Category("fashion");
            jean = new Product("jean", 50, fashion);
            hat = new Product("hat", 10, fashion);

            //category food
            food = new Category("food");
            orange = new Product("orange", 20, food);
            apple = new Product("apple", 2, food);

            //campaign
            campaign1 = new Campaign(fashion, 50, 3, new DiscountTypeRate() { });
            campaign2 = new Campaign(fashion, 40, 5, new DiscountTypeAmount() { });
            campaign3 = new Campaign(food, 10, 20, new DiscountTypeRate() { });

        }

        [Fact]
        public void AddItem_UpdateQuantity_WhenQuantityAddedInSameCategory()
        {
            _shoppingCart.AddItem(jean, 5);
            _shoppingCart.AddItem(jean, 10);
            _shoppingCart.AddItem(jean, 15);

            Assert.Equal(30, _shoppingCart.Products[jean.Title].Quantity);
        }

        [Fact]
        public void GetCampaignDiscount_GiveCorrectDiscountAmount()
        {
            // jean:  50 x 10 = 500 
            // apple: 2 x 50 = 100 

            _shoppingCart.AddItem(jean, 10);
            _shoppingCart.AddItem(apple, 50);
            _shoppingCart.ApplyDiscounts(campaign1, campaign2, campaign3);
            _shoppingCart.GetTotalAmountAfterDiscounts();

            double campaignDiscount = _shoppingCart.GetCampaignDiscount();
            Assert.Equal(250, campaignDiscount);
        }

        [Fact]
        public void GetCouponDiscount_GiveCorrectCouponDiscountAmount()
        {
            // jean:  50 x 10 = 500 
            // apple: 2 x 50 = 100 

            _shoppingCart.AddItem(jean, 10);
            _shoppingCart.AddItem(apple, 50);

            coupon = new Coupon(300, 50, DiscountType.Rate);
            _shoppingCart.ApplyCoupon(coupon);

            _shoppingCart.GetTotalAmountAfterDiscounts();
            double couponDiscount = _shoppingCart.GetCouponDiscount();

            Assert.Equal(300, couponDiscount);

        }
        [Fact]
        public void GetTotalAmountAfterDiscounts_GiveCorrectTotalAmountAfterDiscounts()
        {
            // jean:  50 x 10 = 500 
            // apple: 2 x 50 = 100 

            _shoppingCart.AddItem(jean, 10);
            _shoppingCart.AddItem(apple, 50);

            _shoppingCart.ApplyDiscounts(campaign1, campaign2, campaign3);
            coupon = new Coupon(500, 10, DiscountType.Rate);
            _shoppingCart.ApplyCoupon(coupon);

            double totalAmountAfterDiscount = _shoppingCart.GetTotalAmountAfterDiscounts();

            Assert.Equal(290, totalAmountAfterDiscount);
        }

    }
}

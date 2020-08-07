using ConsoleTables;
using ShoppingCart.Business.Abstract;
using ShoppingCart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart
{
    public class ShoppingCart : IShoppingCart
    {
        private IDeliveryCostCalculator _deliveryCostCalculator;
        public IDictionary<string, Product> Products { get; set; }

        private Coupon coupon;
        private List<Campaign> campaigns;

        private double couponDiscount = 0;
        private double campaignDiscount = 0;
        private double cartTotalAmount = 0;
        private double cartTotalAmountAfterDiscounts = 0;


        public ShoppingCart(IDeliveryCostCalculator deliveryCostCalculator)
        {
            _deliveryCostCalculator = deliveryCostCalculator;
            Products = new Dictionary<string, Product>();
        }
        public double GetCampaignDiscount() => campaignDiscount;
        public double GetCouponDiscount() => couponDiscount;
        public double GetDeliveryCost() => _deliveryCostCalculator.CalculateFor(this);
        public int GetNumberOfDeliveries() => Products.GroupBy(x => x.Value.Category.Title).Distinct().Count();
        public int GetNumberOfProducts() => Products.Count;
        public void AddItem(Product product, int quantity)
        {
            if (product == null || quantity < 1 || product.Category == null || string.IsNullOrEmpty(product.Title))
                return;

            if (!Products.ContainsKey(product.Title))
            {
                product.Quantity = quantity;
                Products.Add(product.Title, product);
            }

            else //ürün ekliyse miktarını güncelle
                Products[product.Title].Quantity += quantity;


            cartTotalAmount += product.UnitPrice * quantity;
        }
        public double GetTotalAmountAfterDiscounts()
        {
            ApplyCampaignDiscount();
            ApplyCouponDiscount();
            return cartTotalAmountAfterDiscounts;
        }
        public void ApplyDiscounts(params Campaign[] discounts)
        {
            campaigns = new List<Campaign>();
            campaigns.AddRange(discounts);
        }
        public void ApplyCoupon(Coupon _coupon)
        {
            coupon = _coupon;
        }
        private void ApplyCouponDiscount()
        {
            if (coupon == null)
                return;

            if (campaigns == null)
                cartTotalAmountAfterDiscounts = cartTotalAmount;

            double calculatedCouponDiscount = 0;
            if (cartTotalAmount > coupon.MinimumCartAmount)
            {
                if (coupon.DiscountType == Domain.Enums.DiscountType.Rate)
                    calculatedCouponDiscount = cartTotalAmount * coupon.DiscountAmount / 100;

                else if (coupon.DiscountType == Domain.Enums.DiscountType.Amount)
                    calculatedCouponDiscount = coupon.DiscountAmount;
            }
            couponDiscount = calculatedCouponDiscount;
            cartTotalAmountAfterDiscounts -= couponDiscount;
        }
        private void ApplyCampaignDiscount()
        {
            if (campaigns == null)
                return;

            cartTotalAmountAfterDiscounts = cartTotalAmount;
            Dictionary<string, Product> campaignProduct = null;
            double calculatedDiscount = 0;
            double maxDiscount = 0;

            foreach (Campaign campaign in campaigns)
            {
                // İndirim uygulanacak kategorinin ürünlerini getir
                Dictionary<string, Product> productsByCategory = GetProductsByCategory(campaign.Category);

                int totalQuantityByCategory = productsByCategory.Sum(x => x.Value.Quantity);
                double totalPriceByCategory = productsByCategory.Sum(x => x.Value.UnitPrice * x.Value.Quantity);

                // sepetimdeki ürün miktarı, kampayadaki tanımlı minimimum ürün miktarından fazlaysa indirim uygula
                if (totalQuantityByCategory > campaign.MinProductQuantity)
                {
                    if (campaign.DiscountType == Domain.Enums.DiscountType.Rate)
                    {
                        calculatedDiscount = totalPriceByCategory * campaign.DiscountAmount / 100;
                        if (calculatedDiscount > maxDiscount)
                        {
                            maxDiscount = calculatedDiscount;
                            campaignProduct = productsByCategory;
                        }
                    }
                    else if (campaign.DiscountType == Domain.Enums.DiscountType.Amount)
                    {
                        if (campaign.DiscountAmount > maxDiscount)
                        {
                            maxDiscount = campaign.DiscountAmount;
                            campaignProduct = productsByCategory;
                        }
                    }
                }
            }
            ApplyCampaignDiscountToProductCategory(campaignProduct, maxDiscount);
            campaignDiscount = maxDiscount;
            cartTotalAmountAfterDiscounts -= maxDiscount;
        }
        public string Print()
        {
            var tableDetail = new ConsoleTable("Category", "Product", "Quantity", "Unit Price", "Total Price", "Total Discount(coupon, campaign)");
            var categories = Products.GroupBy(x => x.Value.Category.Title).ToDictionary(e => e.Key, p => p.ToList());

            foreach (var category in categories)
            {
                foreach (var product in category.Value)
                {
                    tableDetail.AddRow(category.Key, product.Value.Title, product.Value.Quantity, product.Value.UnitPrice, product.Value.TotalPrice, $"{couponDiscount}, {product.Value.Category.CalculatedTotalDiscount} = {couponDiscount + product.Value.Category.CalculatedTotalDiscount}");
                }
            }

            var tableFooter = new ConsoleTable("Total Amount", "Delivery Cost");
            tableFooter.AddRow(cartTotalAmountAfterDiscounts, GetDeliveryCost());
            return tableDetail.ToString() + "\n\n" + tableFooter.ToString();
        }

        private Dictionary<string, Product> GetProductsByCategory(Category campaignCategory) => Products.Where(x => x.Value.Category.Title == campaignCategory.Title).ToDictionary(x => x.Key, x => x.Value);
        private void ApplyCampaignDiscountToProductCategory(Dictionary<string, Product> discountedProducts, double discountAmount)
        {
            if (discountedProducts == null || discountedProducts.Count < 1)
                return;

            discountedProducts.Values.ToList().ForEach(x => x.Category.CalculatedTotalDiscount = discountAmount);
        }
    }


}

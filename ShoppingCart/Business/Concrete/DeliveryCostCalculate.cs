using ShoppingCart.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain
{
    public class DeliveryCostCalculate : IDeliveryCostCalculator
    {
        private double FixedCost;
        private double CostPerDelivery { get; set; }
        private double CostPerProduct { get; set; }

        public DeliveryCostCalculate(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }

        public double CalculateFor(ShoppingCart shoppingCart)
        {
            int NumberOfDeliveries = shoppingCart.GetNumberOfDeliveries();
            int numberOfProduct = shoppingCart.GetNumberOfProducts();
            double deliveryCost = (CostPerDelivery * NumberOfDeliveries) + (CostPerProduct * numberOfProduct) + FixedCost;

            return deliveryCost;
        }
    }
}

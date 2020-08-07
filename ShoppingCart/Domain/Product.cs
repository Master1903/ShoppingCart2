using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart
{
    public class Product
    {

        public string Title { get; set; }
        public double UnitPrice { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => Quantity * UnitPrice - Category.CalculatedTotalDiscount;

        public Product(string title, double unitPrice, Category category)
        {
            Title = title;
            UnitPrice = unitPrice;
            Category = category;
        }
    }
}

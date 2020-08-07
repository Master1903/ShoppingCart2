using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart
{
    public class Category
    {
        public string Title { get; set; }
        public Category ParentCategory { get; set; }
        public double CalculatedTotalDiscount { get; set; }

        public Category(string title)
        {
            Title = title;
        }

        public Category(string title, Category parentCategory)
        {
            Title = title;
            ParentCategory = parentCategory;
        }


    }
}

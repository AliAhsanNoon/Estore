﻿using com.Entities;
using com.web.Models;
using System.Collections.Generic;

namespace com.web.ViewModels
{
    public class FilterProductsViewModel
    {
        public List<Product> Products { get; set; }
        public int? CategoryID { get; set; }
        public Pager Pager { get; set; }
    }

    public class CheckOutViewModel
    {
        public List<Product> vCartProducts { get; set; }
        public List<int> vCartProductID { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class ShopViewModel
    {
        public Pager Pager { get; set; }

        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }

        public int MaxPrice { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }

        public string SearchTerm { get; set; }
    }
}
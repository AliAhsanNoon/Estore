using com.Entities;
using System.Collections.Generic;

namespace com.web.ViewModels
{
    public class NewProductViewModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public bool isFeatured { get; set; }
        public string ImageURL { get; set; }
        public List<Category> CategoryList { get; set; }
    }

    public class UpdateProductViewModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public bool isFeatured { get; set; }
        public string ImageURL { get; set; }
        public List<Category> CategoryList { get; set; }
    }

    public class ProductViewModels{
        public Product Product { get; set; }
    }
}
using com.Entities;
using System.Collections.Generic;


namespace com.web.ViewModels
{
    public class ProductWidgetViewModel
    {
        public List<Product> Products { get; set; }
        public bool isLatestProduct { get; set; }
    }
}
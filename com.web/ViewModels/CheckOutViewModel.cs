using com.Entities;
using System.Collections.Generic;

namespace com.web.ViewModels
{
    public class CheckOutViewModel
    {
        public List<Product> vCartProducts { get; set; }
        public List<int> vCartProductID { get; set; }
    }
}
using com.services;
using com.web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class ShopController : Controller
    {
       public ActionResult Index(string SearchTerm, int? minPrice, int? maxPrice, int? CategoryId, int? sortBy)
       {
            var _model = new ShopViewModel
            {
                MaxPrice = ProductService.Instance.GetMaxPrice(),
                FeaturedCategories = CategoryService.Instance.GetFeaturedCategories(),
                Products = ProductService.Instance.SearchProducts(SearchTerm, minPrice.Value, maxPrice.Value, CategoryId.Value, sortBy.Value)
            };
            return View();
       }

        public ActionResult CheckOut()
        {
            CheckOutViewModel viewModel = new CheckOutViewModel();
            var productCookie = Request.Cookies["ProductsCart"].Value;
            if(productCookie != null)
            {
                var pId = productCookie.Split('-').Select(x => int.Parse(x)).ToList();

                viewModel.vCartProducts = ProductService.Instance.GetProducts(pId);
                viewModel.vCartProductID = pId;
            }
            return View(viewModel);
        }
    }
}
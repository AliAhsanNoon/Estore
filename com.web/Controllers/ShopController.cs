using com.services;
using com.web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class ShopController : Controller
    {
       public ActionResult Index(string searchTerm, int? minPrice, int? maxPrice, int? CategoryId, int? sortBy)
       {
            ShopViewModel _model = new ShopViewModel();
            //_model = new ShopViewModel
            //{
            _model.SearchTerm = searchTerm;
            _model.MaxPrice = ProductService.Instance.GetMaxPrice();
            _model.FeaturedCategories = CategoryService.Instance.GetFeaturedCategories();
            _model.Products = ProductService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, CategoryId, sortBy);
            //};
            return View(_model);
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
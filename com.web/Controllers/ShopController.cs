using com.services;
using com.web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class ShopController : Controller
    {
       public ActionResult Index(string searchTerm, int? minPrice, int? maxPrice, int? CategoryId, int? sortBy, int? pageNo)
       {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            ShopViewModel _model = new ShopViewModel();

            _model.SortBy = sortBy;
            _model.CategoryID = CategoryId;
            _model.SearchTerm = searchTerm;
            _model.MaxPrice = ProductService.Instance.GetMaxPrice();
            _model.FeaturedCategories = CategoryService.Instance.GetFeaturedCategories();
            _model.Products = ProductService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, CategoryId, sortBy, pageNo.Value, 10);

            int totalItem = ProductService.Instance.SearchProductsCount(searchTerm, minPrice, maxPrice, CategoryId, sortBy);
            _model.Pager = new Pager(totalItem, pageNo);

            return View(_model);
       }

       public ActionResult FilterProducts(string searchTerm, int? minPrice, int? maxPrice, int? CategoryId, int? sortBy, int? pageNo)
       {
            int totalItem = ProductService.Instance.SearchProductsCount(searchTerm, minPrice, maxPrice, CategoryId, sortBy);

            FilterProductsViewModel _model = new FilterProductsViewModel();

            _model.Pager = new Pager(totalItem, pageNo);
            _model.Products = ProductService.Instance.SearchProducts(searchTerm, minPrice, maxPrice, CategoryId, sortBy, pageNo.Value, 10);

            return PartialView(_model);
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
using com.services;
using com.web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
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
                viewModel.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(viewModel);
       }
    }
}
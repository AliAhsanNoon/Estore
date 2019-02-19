using com.services;
using com.web.ViewModels;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var featuredCat = CategoryService.Instance.GetFeaturedCategories();
            var newPro = ProductService.Instance.NewProduct();
            var newProdx = ProductService.Instance.NewProductx();
            var _homeViewModel = new HomeViewModel
            {
                FeaturedCategories = featuredCat,
               // NewProducts = newPro,
               // NewProductx = newProdx
            };
            
            return View(_homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
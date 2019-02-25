using com.services;
using com.web.ViewModels;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var _model = new HomeViewModel
            {
                FeaturedCategories = CategoryService.Instance.GetFeaturedCategories(),
            };
            return View(_model);
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
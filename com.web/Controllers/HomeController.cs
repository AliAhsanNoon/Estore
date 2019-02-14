using com.services;
using com.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class HomeController : Controller
    {
        CategoryService categoryService = new CategoryService();
        ProductService productService = new ProductService();
        public ActionResult Index()
        {
            var featuredCat = categoryService.GetFeaturedCategories();
            var newPro = productService.NewProduct();
            var newProdx = productService.NewProductx();
            var _homeViewModel = new HomeViewModel
            {
                FeaturedCategories = featuredCat,
                NewProducts = newPro,
                NewProductx = newProdx
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
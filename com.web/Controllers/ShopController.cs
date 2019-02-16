using com.services;
using com.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class ShopController : Controller
    {
        CheckOutViewModel viewModel = new CheckOutViewModel();
        public ActionResult CheckOut()
        {
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
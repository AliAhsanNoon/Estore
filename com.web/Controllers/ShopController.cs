using com.services;
using com.web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class ShopController : Controller
    {
        ProductService productService = new ProductService();
        CheckOutViewModel viewModel = new CheckOutViewModel();

        public ActionResult CheckOut()
        {
            var productCookie = Request.Cookies["ProductsCart"].Value;
            if(productCookie != null)
            {
                var pId = productCookie.Split('-').Select(x => int.Parse(x)).ToList();

                viewModel.vCartProducts = productService.GetProducts(pId);
                viewModel.vCartProductID = pId;
            }
            return View(viewModel);
        }
    }
}
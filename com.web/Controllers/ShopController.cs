using com.Entities;
using com.services;
using com.web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
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

        public JsonResult PlaceOrder(string productIds)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIds))
            {
                var productQty = productIds.Split('-').Select(x => int.Parse(x)).ToList();
                var soldProduct = ProductService.Instance.GetProducts(productQty.Distinct().ToList());

                var newOrder = new Order();

                newOrder.Status = "Pending";
                newOrder.OrderedAt = DateTime.Now;
                newOrder.UserId = User.Identity.GetUserId();
                newOrder.TotalAmount = soldProduct.Sum(x => x.Price * productQty.Where(pId => pId == x.ID).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(soldProduct
                    .Select(x => new OrderItem
                        {
                            ProductID = x.ID,
                            Quantity = productQty.Where(p => p == x.ID).Count()
                        }
                    ));

                var rowsEffected = ShopService.Instance.SaveOrders(newOrder);
                result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false, Rows = 0 };
            }

            return result;
        }
    }
}
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
    public class AdminController : Controller
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

        public ActionResult Index(string userId, string status, int? pageNo,int? pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = OrderService.Instance.SearchOrdersCount(userId, status);

            AdminViewModel _model = new AdminViewModel();

            _model.UserId = userId;
            _model.Status = status;
            _model.Pager = new Pager(totalRecords, pageNo, 10);
            _model.Orders = OrderService.Instance.SearchOrders(userId, status, pageNo.Value, 10);
            
            return View(_model);
        }

        public ActionResult Details(int Id)
        {

            AdminDetailsViewModel _model = new AdminDetailsViewModel();
            _model.Order = OrderService.Instance.GetOrderById(Id);
            if (_model.Order != null)
            {
                _model.OrderBy = UserManager.FindById(_model.Order.UserId);
            }

            _model.OrderStatus = new List<string>() { "Pending", "In Progress", "Delivered" };
            return View(_model);
        }

        public JsonResult ChangeStatus(int id,string status)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = OrderService.Instance.UpdateOrderStatus(id, status) };

            return result;
        }
    }
}
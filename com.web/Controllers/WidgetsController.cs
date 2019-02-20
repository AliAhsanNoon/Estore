using com.services;
using com.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class WidgetsController : Controller
    {
        public ActionResult Products(bool isLatestProduct)
        {
            var _model = new ProductWidgetViewModel();
            if (isLatestProduct)
            {
                _model.Products = ProductService.Instance.NewProduct();
            }
            else
            {
                _model.Products = ProductService.Instance.GetProducts(1,4);
            }
            return PartialView(_model);
        }
    }
}
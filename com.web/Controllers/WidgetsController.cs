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
        public ActionResult Products(bool isLatestProduct, int? categoryID = 0)
        {
            var _model = new ProductWidgetViewModel();
            _model.isLatestProduct = isLatestProduct;
            if (isLatestProduct)
            {
                _model.Products = ProductService.Instance.NewProduct();
            }
            else if (categoryID.HasValue && categoryID.Value > 0)
            {
                _model.Products = ProductService.Instance.RelatedProducts(categoryID.Value);
            }
            else
            {
                _model.Products = ProductService.Instance.GetProducts(1,8);
            }
            return PartialView(_model);
        }
    }
}
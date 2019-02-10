using com.Entities;
using com.services;
using com.web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace com.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly bool success = true;

        ProductService productService = new ProductService();
        CategoryService category = new CategoryService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var editModel = productService.GetProduct(id);
            return View(editModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModels productVM)
        {
            var _product = productService.GetProduct(productVM.ID);
            _product.ID = productVM.ID;
            _product.Name = productVM.Name;
            _product.Description = productVM.Description;
            _product.isFeatured = productVM.isFeatured;
            _product.ImageURL = productVM.ImageURL;
            productService.UpdateProducts(_product);
            return Json(new { success },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var del = productService.GetProduct(id);
            productService.DeleteProducts(del.ID);
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductTable()
        {
            var products = productService.GetProducts();

            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(int id = 0)
        {
            var editModel = productService.GetProduct(id);
            var _catList = category.GetCategories();
            return View("CreateOrUpdate", _catList);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(ProductViewModels viewModel)
        {
            bool setTrue;
            if(viewModel.isFeatured) { setTrue = true; }
            else { setTrue = false; }
            var pinDB = new Product();

            pinDB.Name = viewModel.Name;
            pinDB.Description = viewModel.Description;
            pinDB.Price = viewModel.Price;
            pinDB.Category = category.Edit(viewModel.Category_ID);
            pinDB.isFeatured = setTrue;//viewModel.isFeatured;
            pinDB.ImageURL = viewModel.ImageURL;
            productService.SaveProducts(pinDB);
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

    }
}

using com.Entities;
using com.services;
using com.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class ProductController : Controller
    {

        ProductService productService = new ProductService();
        CategoryService category = new CategoryService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(category.GetCategories());
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModels viewModel)
        {
            var pinDB = new Product();

            pinDB.Name = viewModel.Name;
            pinDB.Description = viewModel.Description;
            pinDB.Price = viewModel.Price;
            pinDB.Category = category.Edit(viewModel.Category_ID);
            pinDB.isFeatured = viewModel.isFeatured;
            pinDB.ImageURL = viewModel.ImageURL;
            productService.SaveProducts(pinDB);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var editModel = productService.GetProduct(id);
            return PartialView(editModel);
        }

        [HttpPost]
        public ActionResult Edit(UpdateProductViewModels productVM)
        {
            var _product = productService.GetProduct(productVM.ID);
            _product.ID = productVM.ID;
            _product.Name = productVM.Name;
            _product.Description = productVM.Description;
            _product.isFeatured = productVM.isFeatured;
            _product.ImageURL = productVM.ImageURL;
            productService.UpdateProducts(_product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //var del = productService.GetProduct(id);
            productService.DeleteProducts(id);
            return RedirectToAction("ProductTable");
        }

        public ActionResult ProductTable()
        {
            var products = productService.GetProducts();

            return PartialView(products);
        }

    }
}

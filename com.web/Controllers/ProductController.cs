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
            var _viewModel = new NewProductViewModels
            {
                CategoryList = category.GetCategories()
            };

            return PartialView(_viewModel);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModels viewModel)
        {
            var newProduct = new Product();

            newProduct.Name = viewModel.Name;
            newProduct.Description = viewModel.Description;
            newProduct.Price = viewModel.Price;
            newProduct.Category = category.Edit(viewModel.CategoryID);
            newProduct.isFeatured = viewModel.isFeatured;
            newProduct.ImageURL = viewModel.ImageURL;
            productService.SaveProducts(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var prod = productService.GetProduct(id);
            UpdateProductViewModels editModel = new UpdateProductViewModels ();
            editModel.ID = prod.ID;
            editModel.Name = prod.Name;
            editModel.Description = prod.Description;
            editModel.Price = prod.Price;
            editModel.CategoryID = prod.Category != null ? prod.Category.ID : 0;
            editModel.CategoryList = category.GetCategories();
            editModel.ImageURL = prod.ImageURL;
            editModel.isFeatured = prod.isFeatured;
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
            _product.Category = null;
            _product.CategoryID = productVM.CategoryID;
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

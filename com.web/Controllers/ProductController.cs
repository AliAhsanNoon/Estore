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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var _viewModel = new NewProductViewModels
            {
                CategoryList = CategoryService.Instance.GetCategories()
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
            newProduct.Category = CategoryService.Instance.Edit(viewModel.CategoryID);
            newProduct.isFeatured = viewModel.isFeatured;
            newProduct.ImageURL = viewModel.ImageURL;
            ProductService.Instance.SaveProducts(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var prod = ProductService.Instance.GetProduct(id);
            UpdateProductViewModels editModel = new UpdateProductViewModels ();
            editModel.ID = prod.ID;
            editModel.Name = prod.Name;
            editModel.Description = prod.Description;
            editModel.Price = prod.Price;
            editModel.CategoryID = prod.Category != null ? prod.Category.ID : 0;
            editModel.CategoryList = CategoryService.Instance.GetCategories();
            editModel.ImageURL = prod.ImageURL;
            editModel.isFeatured = prod.isFeatured; 
            return PartialView(editModel);
        }

        [HttpPost]
        public ActionResult Edit(UpdateProductViewModels productVM)
        {
             var _product = ProductService.Instance.GetProduct(productVM.ID);
            _product.ID = productVM.ID;
            _product.Name = productVM.Name;
            _product.Description = productVM.Description;
            _product.isFeatured = productVM.isFeatured;
            _product.ImageURL = productVM.ImageURL;
            _product.Category = null;
            _product.Category = CategoryService.Instance.Edit(productVM.CategoryID);
            ProductService.Instance.UpdateProducts(_product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ProductService.Instance.DeleteProducts(id);
            return RedirectToAction("ProductTable");
        }

        public ActionResult ProductTable()
        {
            var products = ProductService.Instance.GetProducts();

            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            var _model = new ProductViewModels
            {
                Product = ProductService.Instance.GetProduct(ID)
            };
            return View(_model);
        }
    }
}

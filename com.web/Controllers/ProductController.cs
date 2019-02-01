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
        // GET: Product
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            //var selectCategory = category.GetCategories();
            //var newViewModel = new ProductViewModels {
            //    Categories = selectCategory,
            //    Product = new Product()
            //};

            return View(category.GetCategories());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModels viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                var pModel = new Product();
                pModel.Name = viewModel.Name;
                pModel.Description = viewModel.Description;
                pModel.Price = viewModel.Price;
                pModel.Category = category.Edit(viewModel.Category_ID);
                productService.SaveProducts(pModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProductTable()
        {
            return View(productService.GetProducts());
        }
    }
}

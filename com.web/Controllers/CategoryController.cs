using com.Entities;
using com.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.web.Controllers
{
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();
        // GET: Category
        public ActionResult Index()
        {
            return View(categoryService.GetCategories());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category collection)
        {
            try
            {
                categoryService.SaveCategory(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(categoryService.Edit(id));
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category collection)
        {
            try
            {
                // TODO: Add update logic here
                categoryService.updateCategory(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(categoryService.Edit(id));
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(Category collection)
        {
            try
            {
                // TODO: Add delete logic here
                categoryService.Delete(collection.ID);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

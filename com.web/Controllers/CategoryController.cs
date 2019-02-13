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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            categoryService.SaveCategory(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return PartialView(categoryService.Edit(id));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.updateCategory(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                categoryService.Delete(id);
                return RedirectToAction("CategoryTable");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CategoryTable()
        {
            var cat = categoryService.GetCategories();
            return PartialView(cat); 
        }

    }
}

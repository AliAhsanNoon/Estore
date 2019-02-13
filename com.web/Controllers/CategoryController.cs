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
        private readonly bool success = true;
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
            return View(categoryService.Edit(id));
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

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateOrUpdate(int id=0 )
        {
            var c = categoryService.Edit(id);
            return View(c);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(Category category)
        {
            if(category.ID == 0)
            {
                categoryService.SaveCategory(category);
                return Json(new { success }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                categoryService.updateCategory(category);
                return Json(new { success  }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                categoryService.Delete(id);
                return Json(new { success }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CategoryTable()
        {
            var cat = categoryService.GetCategories();
            return Json(new { data = cat}, JsonRequestBehavior.AllowGet); 
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

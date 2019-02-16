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
            CategoryService.Instance.SaveCategory(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return PartialView(CategoryService.Instance.Edit(id));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoryService.Instance.updateCategory(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            CategoryService.Instance.Delete(id);
                return RedirectToAction("CategoryTable");
        }

        public ActionResult CategoryTable()
        {
            var cat = CategoryService.Instance.GetCategories();
            return PartialView(cat); 
        }

    }
}

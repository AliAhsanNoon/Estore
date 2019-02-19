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

        public ActionResult CategoryTable(int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = CategoryService.Instance.GetTotalItem();

            if(CategoryService.Instance.GetCategories() != null)
            {
                var pager = new Pager(totalRecords, pageNo, 3);
            }

            var cat = CategoryService.Instance.GetCategories();
            return PartialView(cat); 
        }

    }
}

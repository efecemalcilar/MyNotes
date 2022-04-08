using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using MyNotes.BusinessLayer;
using MyNotes.BusinessLayer.Models;
using MyNotes.BusinessLayer.ValueObject;
using MyNotes.EntityLayer;

namespace MyNotes.MVC.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager();

        // GET: Category

        
        public ActionResult Index()
        {

            var cat = cm.IndexCat();
            return View(cat);
            //return View(cm.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            //var cat = cm.Find(s => s.Id == id);
            CategoryViewModel cvm = cm.FindCat(id);
            if (cvm == null)
            {
                return HttpNotFound();
            }
            //CategoryViewModel cvm = new CategoryViewModel();
            //cvm.Category.Id = cat.Id;
            //cvm.Category.Tittle = cat.Tittle;
            //cvm.Category.Description = cat.Description;
            //cvm.Category.ModifiedUserName = cat.ModifiedUserName;
            //cvm.Category.CreatedOn = cat.CreatedOn;
            //cvm.Category.ModifiedOn = cat.ModifiedOn;

            return View(cvm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel cat)
        {
            ModelState.Remove("Category.CreatedOn");
            ModelState.Remove("Category.ModifiedOn");
            ModelState.Remove("Category.ModifiedUserName");

            if (ModelState.IsValid) //ModelStatle ile birlikte modifiedon Createon ModifiedUser gelecek.
            {
                cm.InsertCat(cat);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }

            return View(cat);
        }
    }
}
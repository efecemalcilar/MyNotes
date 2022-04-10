using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }

            CategoryViewModel category = cm.GetEditCat(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel cat)
        {
            ModelState.Remove("Category.CreatedOn"); // Bu içini artık modelstate de kontrol etme diyor.
            ModelState.Remove("Category.ModifiedOn");
            ModelState.Remove("Category.ModifiedUserName");

            if (ModelState.IsValid)
            {
                //CategoryViewModel category = cm.GetEditCat(cat.Category.Id);
                //category.Category.Tittle = cat.Category.Tittle;
                //category.Category.Description = cat.Category.Description;
                //Category cat1 = new Category();
                //cat1.Tittle = cat.Category.Tittle;
                //cat1.Description = cat.Category.Description;
                
                cm.UpdateCat(cat);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
                  
            }

            return View(cat); 
        }

        public ActionResult Delete(int? id) // Bir Get işlemi
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            CategoryViewModel category = cm.GetEditCat(id);

            if (category==null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost,ActionName("Delete")] //ActionName i Delete olan get işleminin post işlemidir dedik.

        public ActionResult DeleteConfirmed(int id) //Bir post işlemi Post ismi Get işlemde ki isimle aynı olur. Ama biz farklı isimle tanımladık.
        {
            cm.DeleteCat(id);
            CacheHelper.RemoveCategoriesFromCache();
            return RedirectToAction("Index");
        }
    }
    
}
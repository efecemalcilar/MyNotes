using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNotes.BusinessLayer;
using MyNotes.BusinessLayer.Models;
using MyNotes.EntityLayer;
using MyNotesDataAccessLayer;

namespace MyNotes.MVC.Controllers
{
    public class NoteController : Controller
    {
        //private MyNoteContext db = new MyNoteContext();
       
        private NoteManager nm = new NoteManager();
        private CategoryManager cm = new CategoryManager();
        private LikedManager lm = new LikedManager();

        // GET: Note
        public ActionResult Index() //Notelarımı listelemek istiyorum ve bunun için Index i kullanıcam.
        {
            List<Note> notes = nm.QList().Include("Category").Include("Owner")
                .Where(x => x.Owner.Id == CurrentSession.User.Id).OrderByDescending(s => s.ModifiedOn).ToList();   //Include kullanabilmek i,çin 2 tablo arasında foreign key bağlantısı olmalıdır.
            
            return View(notes);
        }

        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = nm.Find(s=>s.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Note/Create
        public ActionResult Create() //Amacım create sayfası oluştururken ilgili kategori kısmının içerisindeki verileri id ye göre seçip onları titlelarına göre bana verecek şekilde design etmiş oldum.
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Tittle");
            return View();
        }

        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Id,Tittle,Text,isDraft,LikeCount,CreatedOn,ModifiedOn,ModifiedUserName")]*/ Note note) //Bind datasının amacı İçerde bana hangi elemanların oldugunu declare eder
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
             ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                note.Owner = CurrentSession.User;
                
                
                nm.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Tittle",note.CategoryId);
            return View(note);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = nm.Find(s => s.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Tittle", note.CategoryId);
            return View(note);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Note note)
        {
            if (ModelState.IsValid)
            {
                ModelState.Remove("CreatedOn");
                ModelState.Remove("ModifiedOn");
                ModelState.Remove("ModifiedUserName");

                Note dbNote = nm.Find(s => s.Id == note.Id);
                dbNote.isDraft = note.isDraft;
                dbNote.CategoryId = note.Category.Id;
                dbNote.Text = note.Text;
                dbNote.Tittle = note.Tittle;
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Tittle", note.CategoryId);
            return View(note);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = nm.Find(s => s.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = nm.Find(s => s.Id == id);
            nm.Delete(note);
            return RedirectToAction("Index");
        }

        
    }
}

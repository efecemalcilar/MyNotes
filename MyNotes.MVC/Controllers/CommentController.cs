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
    public class CommentController : Controller
    {
        //private MyNoteContext db = new MyNoteContext();

        private NoteManager nm = new NoteManager();

        private CommandMaanager cm = new CommandMaanager();

        // GET: Comment
        public ActionResult Index()
        {
            return View(cm.List());
        }

        // GET: Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = cm.Find(x =>x.Id == id );
            
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment,int? noteId) //Create in amacı yoruma tıkladığımızda ilgili notun Id si ile gelsin ki ben onu oluşturayım.
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                if (noteId==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Note note = nm.Find(s => s.Id == noteId);

                if (note == null)
                {
                    return new HttpNotFoundResult();
                }

                comment.Note = note;
                comment.Owner = CurrentSession.User;

                if (cm.Insert(comment)>0)
                {
                    return Json(new {result = true}, JsonRequestBehavior.AllowGet);
                }
            }


            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = cm.Find(s => s.Id == id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        
        [HttpPost]
        
        public ActionResult Edit(int? id , string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = cm.Find(s => s.Id == id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            comment.Text = text;

            if (cm.Update(comment)>0)
            {
                return Json(new {result = true}, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);


        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = cm.Find(s => s.Id == id);


            if (cm.Delete(comment) > 0)
            {
                return Json(new {result=true} , JsonRequestBehavior.AllowGet);
            }

            return Json(new {result = false}, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult ShowNoteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = nm.QList().Include("Comments").FirstOrDefault(s => s.Id == id);
            
            if (note == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialComments", note.Comments); // Id si bir olan notun diyelim ki 10 tane yorumu var burda id si 1 olan notun 10 yorumunu bana gönder diyor.
        }

        
    }
}

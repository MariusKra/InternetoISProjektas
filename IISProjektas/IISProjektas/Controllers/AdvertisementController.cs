using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISProjektas.Controllers
{
    public class AdvertisementController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Advertisement/
        /*
        public ActionResult Index()
        {

            var advertisements = db.Advertisements.Include(a => a.Category).Include(a => a.User);
            return View(advertisements.ToList());
        }*/

        //
        // GET: /Advertisement/Details/5
        
        public ActionResult Details(int id = 0)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        //
        // GET: /Advertisement/Create
        /*
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name");
            ViewBag.user_id = new SelectList(db.Users, "Id", "username");
            return View();
        }*/

        //
        // POST: /Advertisement/Create
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "Id", "name", advertisement.category_id);
            ViewBag.user_id = new SelectList(db.Users, "Id", "username", advertisement.user_id);
            return View(advertisement);
        }

        //
        // GET: /Advertisement/Edit/5
        */
        public ActionResult Edit(int id = 0)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name", advertisement.category_id);
            ViewBag.user_id = new SelectList(db.Users, "Id", "username", advertisement.user_id);
            return View(advertisement);
        }

        //
        // POST: /Advertisement/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name", advertisement.category_id);
            ViewBag.user_id = new SelectList(db.Users, "Id", "username", advertisement.user_id);
            return View(advertisement);
        }

        //
        // GET: /Advertisement/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        //
        // POST: /Advertisement/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            db.Advertisements.Remove(advertisement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
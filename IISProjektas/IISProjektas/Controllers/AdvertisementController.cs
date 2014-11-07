using IISProjektas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

            double rate = 0;
            var rateList = db.Ratings.Where(x => x.advertisement_id == advertisement.Id).ToList();
            bool rated = true;

            if (Session["LogedUserID"] != null)
            {
                int userId = int.Parse(Session["LogedUserID"].ToString());

                if(advertisement.user_id == userId){
                    rated = true;
                }
                else{
                    rated = rateList.Where(x => x.User1.Id == userId).ToList().Count > 0 ? true : false;
                }
            }
           



            if (rateList.Count != 0)
            {

                rate = (double)rateList.Average(y => y.rating1);
            }

            List<Comment> commentsList = db.Comments
                .Where(x => x.advertisement_id == advertisement.Id)
                .OrderBy(x => x.date_created).ToList();

            List<CommentModel> commentsModelList = commentsList
                .Select(x => new CommentModel()
                {
                    CommentId = x.Id,
                    date_created = ((DateTime)x.date_created).ToString("yyyy-MM-dd"),
                    text = x.text,
                    username = x.User.username
                }).ToList();

            

              //  db.Comments.Where(x => x.advertisement_id == advertisement.Id).OrderByDescending(x => x.date_created).ToList()
            AdvertisementModel model = new AdvertisementModel()
            {
                Id = advertisement.Id,
                author = advertisement.User.username,
                author_id = advertisement.user_id,
                category = advertisement.Category.name,
                dateCreated = advertisement.date_created.ToString("yyyy-MM-dd"),
                description = advertisement.description,
                image = System.IO.File.ReadAllBytes(advertisement.Image),
                name = advertisement.name,
                commentRating =  double.Parse(String.Format("{0:0.00}", rate)),
                isRated = rated,
                comments = commentsModelList
            };
            List<SelectListItem> dropDown = new List<SelectListItem>(){
                new SelectListItem(){Text = "1", Value = "1", Selected = true},
                new SelectListItem(){Text = "2", Value = "2"},
                new SelectListItem(){Text = "3", Value = "3"},
                new SelectListItem(){Text = "4", Value = "4"},
                new SelectListItem(){Text = "5", Value = "5"}
            };

            ViewBag.commentDropDownList = dropDown;

            return View(model);
        }
       
        public ActionResult RateAdvertisement(int commentDropDown, int Id)
        {
            int userId = int.Parse(Session["LogedUserID"].ToString());
            Rating rate = new Rating()
            {
                advertisement_id = Id,
                user = userId,
                rating1 = commentDropDown

            };

            db.Ratings.Add(rate);
            db.SaveChanges();

            double avg = 0;
            var rateList = db.Ratings.Where(x => x.advertisement_id == Id).ToList();
            if (rateList.Count != 0)
            {

                avg = (double)rateList.Average(y => y.rating1);
            }
            avg = double.Parse(String.Format("{0:0.00}", avg));
            return Json(avg);
        }

        public ActionResult WriteComment(string commentText, int Id)
        {
            Comment comment = new Comment()
            {
                user_id = int.Parse(Session["LogedUserID"].ToString()),
                text = commentText,
                advertisement_id = Id,
                date_created = DateTime.Now 

            };

            db.Comments.Add(comment);
            db.SaveChanges();



            return Json(((DateTime)comment.date_created).ToString("yyyy-MM-dd"));
        }

        public ActionResult DeleteComment(int Id)
        {
            db.Comments.Remove(db.Comments.Find(Id));
            db.SaveChanges();

            return Json(Id);
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

            AdvertisementEditModel model = new AdvertisementEditModel()
            {
                Id = advertisement.Id,
                category_id = advertisement.Category.Id,               
                description = advertisement.description,
                imageBytes = System.IO.File.ReadAllBytes(advertisement.Image),
                name = advertisement.name


            };

            model.categoryDropDown = db.Categories.ToList().Select(z => new SelectListItem()
                {
                    Text = z.name,
                    Value = z.Id.ToString()
                }).ToList();

            
            return View(model);
        }

        //
        // POST: /Advertisement/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdvertisementEditModel model)
        {
            if (ModelState.IsValid)
            {
                Advertisement advertisement = db.Advertisements.Find(model.Id);

                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(model.Image.FileName);
                    string file = String.Format("{0}{1}", DateTime.Now.Millisecond, fileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Images"), file);
                    model.Image.SaveAs(path);
                    advertisement.Image = path;
                }

                advertisement.description = model.description;
                advertisement.category_id = model.category_id;
                advertisement.name = model.name;
               


                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();

                   // return Redirect("/Advertisement")
                return RedirectToAction("Details", "Advertisement", new { id = model.Id });                

                
            }
            ModelState.AddModelError("","Kuriant skelbimą įvyko klaida");

            return View(model);
        }

        //
        // GET: /Advertisement/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Advertisement advertisement = db.Advertisements.Find(id);

            AdvertisementModel model = new AdvertisementModel()
            {
                Id = advertisement.Id,
                author = advertisement.User.username,
                author_id = advertisement.user_id,
                category = advertisement.Category.name,
                dateCreated = advertisement.date_created.ToString("yyyy-MM-dd"),
                description = advertisement.description,
                image = System.IO.File.ReadAllBytes(advertisement.Image),
                name = advertisement.name

            };


            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //
        // POST: /Advertisement/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            List<Rating> ratings = db.Ratings.Where(x => x.advertisement_id == advertisement.Id).ToList();
            foreach (Rating r in ratings)
            {
                db.Ratings.Remove(r);
            }
            List<Comment> comments = db.Comments.Where(x => x.advertisement_id == advertisement.Id).ToList();
            foreach (Comment c in comments)
            {
                db.Comments.Remove(c);
            }


            db.Advertisements.Remove(advertisement);
            db.SaveChanges();
            if (Session["CurrentPage"].ToString() == "Index")
                return RedirectToAction("Index", "Home");
            else return RedirectToAction("MyAds", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
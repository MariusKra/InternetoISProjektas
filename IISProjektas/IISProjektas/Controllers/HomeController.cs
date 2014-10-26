using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IISProjektas.Models;
using System.IO;


namespace IISProjektas.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index(AdvertisementFilterModel modelOld)
        {

        //http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application


            List<AdvertisementModel> list = new List<AdvertisementModel>();

            var ads = db.Advertisements.OrderByDescending(x => x.date_created).ToList();

            AdvertisementFilterModel model = new AdvertisementFilterModel();
            if (!String.IsNullOrEmpty(modelOld.descriptionFilter))
            {
                ads = ads.Where(x => x.description.ToUpper().Contains(modelOld.descriptionFilter.ToUpper())).ToList();

            }
            if (!String.IsNullOrEmpty(modelOld.usernameFilter))
            {
                ads = ads.Where(x => x.User.username.ToUpper().Contains(modelOld.usernameFilter.ToUpper())).ToList();

            }

            if (modelOld.categoryFilter != null && modelOld.categoryFilter > 0)
            {
                ads = ads.Where(x => x.category_id == modelOld.categoryFilter).ToList();
            }


            foreach (Advertisement ad in ads)
            {
                AdvertisementModel mod = new AdvertisementModel();
                mod.Id = ad.Id;
                mod.image = System.IO.File.ReadAllBytes(ad.Image);
                mod.category = ad.Category.name;
                mod.author = ad.User.username;
                mod.description = ad.description;
                mod.name = ad.name;
                mod.author_id = ad.user_id;
                mod.dateCreated = ad.date_created.ToString("yyyy-MM-dd");
                list.Add(mod);
            }

            List<SelectListItem> listas = db.Categories.ToList().Select(z => new SelectListItem()
                 {
                     Text = z.name,
                     Value = z.Id.ToString()
                 }).ToList();
            listas.Add(new SelectListItem() {Value = "0", Text = "", Selected=true });
            model.categoryDropDownList = listas;
            model.categoryFilter = modelOld.categoryFilter;
            model.descriptionFilter = modelOld != null ? modelOld.descriptionFilter : "";
            model.usernameFilter = modelOld != null ? modelOld.usernameFilter : "";

            model.adsList = list;
            return View(model);
        }

        public ActionResult MyAds()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Messages()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {

            AdvertisementCreateModel model = new AdvertisementCreateModel()
            {
                categoryDropDown = db.Categories.ToList().Select(z => new SelectListItem()
                {
                    Text = z.name,
                    Value = z.Id.ToString()
                }).ToList()

            };



            return View(model);
        }

       
        public ActionResult CreateAdvertisement(AdvertisementCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Advertisement ad = new Advertisement()
                {
                    category_id = model.category_id,
                    name = model.name,
                    user_id = int.Parse(Session["LogedUserID"].ToString()),
                    description = model.description,
                    date_created = DateTime.Now
                };



                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(model.Image.FileName);
                    string file = String.Format("{0}{1}", DateTime.Now.Millisecond, fileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Images"), file);
                    model.Image.SaveAs(path);
                    ad.Image = path;
                }
                Response.Cookies.Add(new HttpCookie("AdCreated") { Value = "1", Expires = DateTime.Now.AddSeconds(15) });


                db.Advertisements.Add(ad);
                db.SaveChanges();

                return RedirectToAction("MyAds", "Home");
            }

            Response.Cookies.Add(new HttpCookie("AdCreated") { Value = "2", Expires = DateTime.Now.AddSeconds(15) });

            return RedirectToAction("MyAds", "Home");
        }

    }
}

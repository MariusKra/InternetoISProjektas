using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IISProjektas.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Data;

namespace IISProjektas.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        /*public ActionResult Index2(int? page)
        {
            var ads = db.Advertisements.OrderByDescending(x => x.date_created).ToList();

        
        }*/

        public ActionResult Index(AdvertisementFilterModel modelOld, int? page, string descriptionFilter, string usernameFilter, string categoryFilter)
        {

        //http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application


            List<AdvertisementModel> list = new List<AdvertisementModel>();

            var ads = db.Advertisements.OrderByDescending(x => x.date_created).ToList();

            AdvertisementFilterModel model = new AdvertisementFilterModel();
           
            if (!String.IsNullOrEmpty(descriptionFilter))
            {
                ads = ads.Where(x => x.description.ToUpper().Contains(descriptionFilter.ToUpper())).ToList();


            }

            if (!String.IsNullOrEmpty(usernameFilter))
            {
                ads = ads.Where(x => x.User.username.ToUpper().Contains(usernameFilter.ToUpper())).ToList();

            }

            if (!String.IsNullOrEmpty(categoryFilter) && int.Parse(categoryFilter)>0)
            {
                ads = ads.Where(x => x.category_id == int.Parse(categoryFilter)).ToList();

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

            List<SelectListItem> listas = new List<SelectListItem>();
                listas.Add(new SelectListItem() {Value = "0", Text = "", Selected=true });
                listas = listas.Union(db.Categories.ToList().Select(z => new SelectListItem()
                 {
                     Text = z.name,
                     Value = z.Id.ToString()
                 }).ToList()).ToList();
                
            
            ViewBag.descriptionFilter = descriptionFilter;
            ViewBag.usernameFilter = usernameFilter;
            ViewBag.categoryFilter = categoryFilter;
            ViewBag.categoryDropDownList = listas;


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            model.adsList = list.ToPagedList(pageNumber, pageSize);

            Session["CurrentPage"] = "Index";

            return View(model);
        }

        public ActionResult MyAds(int? page)
        {
            ViewBag.Message = "";

            List<AdvertisementModel> list = new List<AdvertisementModel>();
            int id = int.Parse(Session["LogedUserID"].ToString());
            var ads = db.Advertisements.OrderByDescending(x => x.date_created).Where(x => x.user_id == id).ToList();

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

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            Session["CurrentPage"] = "MyAds";
            return View(list.ToPagedList(pageNumber, pageSize));           

            return View();
        }

        public ActionResult Messages()
        {
            MessageUsersListModel model = new MessageUsersListModel()
            {
                MessagesRecipients = new List<MessageReceiverModel>()
            };
            int currentUserId = int.Parse(Session["LogedUserID"].ToString());
            List<Message> userMessages = db.Messages.Where(x => x.sender_id == currentUserId || x.receiver_id == currentUserId).ToList();


            foreach (User usr in db.Users)
            {
                List<Message> mes = userMessages.Where(x => (x.sender_id == usr.Id || x.receiver_id == usr.Id) && x.MessageState.Id == 1).ToList();

                if (userMessages.Where(x => x.sender_id == usr.Id || x.receiver_id == usr.Id).ToList().Count > 0 && usr.Id != currentUserId)
                {
                    model.MessagesRecipients.Add(new MessageReceiverModel()
                    {
                        Id = usr.Id,
                        receiver = usr.username,
                        isNew = mes.Count > 0 ? true : false

                    });
                }               
            }
            
            return View(model);
        }

        public ActionResult PrivateMessage(int Id)
        {
            int currentUserId = int.Parse(Session["LogedUserID"].ToString());
            List<Message> messages = db.Messages
                .Where(x => x.sender_id == Id && x.receiver_id == currentUserId || 
                    x.receiver_id == Id && x.sender_id == currentUserId).ToList();

            List<MessageModel> messageModel = messages.Select(y => new MessageModel()
                {
                    Id = y.Id,
                    receiver_id = y.receiver_id,
                    sender_id = y.sender_id,
                    text = y.text,
                    state = y.state,
                    sent_date = ((DateTime)y.date_sent).ToString("yyyy-MM-dd"),
                    senderName = y.User1.username
                }).ToList();

            MessageListModel model = new MessageListModel()
            {
                receiver = db.Users.Find(Id).username,                
                Id = Id,                
                messages = messageModel
            };

            for (int i = 0; i < messages.Count; i++ )
            {
                messages[i].state = 2;
                db.Entry(messages[i]).State = EntityState.Modified;

            }
            db.SaveChanges();


            return View(model);
        }


        public ActionResult WriteMessage(int Id)
        {
            MessageModel msg = new MessageModel()
            {
                sender_id = int.Parse(Session["LogedUserID"].ToString()),
                receiver_id = Id,
                text = "",
                state = 1

            };

            ViewBag.username = db.Users.Find(Id).username;

            return View(msg);
        }

        public ActionResult SaveMessage(MessageModel model)
        {
            Message message = new Message()
            {
                sender_id = model.sender_id,
                receiver_id = model.receiver_id,
                text = model.text,
                state = model.state,
                date_sent = DateTime.Now
            };


            db.Messages.Add(message);
            db.SaveChanges();
            Response.Cookies.Add(new HttpCookie("MessageSent") { Value = "1", Expires = DateTime.Now.AddSeconds(15) });


            return RedirectToAction("Index");
        }

        public ActionResult SavePrivateMessage(string MessageText, int Id)
        {
            Message message = new Message()
            {
                sender_id = int.Parse(Session["LogedUserID"].ToString()),
                receiver_id = Id,
                text = MessageText,
                state = 1,
                date_sent = DateTime.Now

            };
            db.Messages.Add(message);
            db.SaveChanges();

            return Json(((DateTime)message.date_sent).ToString("yyyy-MM-dd"));

        }

        public ActionResult DeleteMessage(int Id)
        {
            db.Messages.Remove(db.Messages.Find(Id));
            db.SaveChanges();

            return Json(Id);
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

                if (model.Image != null)
                {
                    db.Advertisements.Add(ad);
                    db.SaveChanges();
                }
                
                return RedirectToAction("MyAds", "Home");
            }

            Response.Cookies.Add(new HttpCookie("AdCreated") { Value = "2", Expires = DateTime.Now.AddSeconds(15) });

            return RedirectToAction("Create", "Home");
        }

        

    }
}

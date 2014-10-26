using IISProjektas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISProjektas.Controllers
{
    public enum Permission
    {
        User = 1,
        Administrator = 2,
        SuperAdministrator = 3
    };
 


    public class UserController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /User/

        public ActionResult Index()
        {
            //var users = db.Users.Include(u => u.UserState);
            var users = db.Users.Select(x => new UserModel()
            {
                Id = x.Id,
                email = x.email,
                //password = x.password,
                permissionsText = db.User_UserGroup.Where(z => z.user_id == x.Id).FirstOrDefault().UserGroup.name,
                stateText = x.UserState.name,
                username = x.username
                
            });

            IEnumerable<UserModel> list = users.ToList();
           
            return View(list);
        }

                //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.state = new SelectList(db.UserStates, "Id", "name");
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.state = new SelectList(db.UserStates, "Id", "name", user.state);
            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);



            if (user == null)
            {
                return HttpNotFound();
            }
            UserEditModel model = new UserEditModel()
            {
                Id = user.Id,
                email = user.email,                
                state = user.state,
                username = user.username,
                password = user.password,
                oldPassword = user.password,
                permissions = db.User_UserGroup.Where(z => z.user_id == user.Id).FirstOrDefault().UserGroup.Id,
                stateDropDown = db.UserStates.ToList()
                .Select(z => new SelectListItem()
                {
                    Text = z.name,
                    Value = z.Id.ToString()
                }).ToList(),
                groupDropDown = db.UserGroups.ToList().Select(z => new SelectListItem()
                {
                    Text = z.name,
                    Value = z.Id.ToString()
                }).ToList(),

            };
            

            return View(model);
        }

        //
        // POST: /User/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(model.Id);
                user.email = model.email;
                user.password = model.password;
                user.state = model.state;
                User_UserGroup group = db.User_UserGroup.Where(x => x.user_id == model.Id).FirstOrDefault();
                group.UserGroup = db.UserGroups.Where(x => x.Id == model.permissions).FirstOrDefault();

                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(model);
        }


        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel U)
        {
            if (ModelState.IsValid)
            {
               
                    if (db.Users.Any(x => x.username.Equals(U.username)))
                    {
                        ViewBag.Error = "Vartotojas tokiu vardu jau egzistuoja";
                        U = null;
                        return View(U);
                    }
                    User user = new User()
                    {
                        username = U.username,
                        password = U.password,
                        email = U.email,
                        state = 1

                    };

                    User_UserGroup user_group = new User_UserGroup()
                    {
                        User = user,
                        UserGroup = db.UserGroups.Where(x => x.Id == 1).FirstOrDefault()
                    };
                   

                    db.Users.Add(user);
                    db.SaveChanges();
                    db.User_UserGroup.Add(user_group);
                    db.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = "Registracija sėkminga";
                
            }
            return View(U);
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                
                    
                     var v = db.Users.Where(a => a.username.Equals(model.username) && a.password.Equals(model.password)).FirstOrDefault();
                if (v != null)
                {
                    Session["LogedUserID"] = v.Id.ToString();
                    Session["LogedUsername"] = v.username.ToString();
                    User_UserGroup tmp = db.User_UserGroup.Where(x => x.User.Id == v.Id).FirstOrDefault();
                    Session["Permissions"] = (int)db.UserGroups.Where(y => y.Id == tmp.UserGroup.Id).FirstOrDefault().Id;
                    return RedirectToAction("Index","Home");
                }
                     
                

                

            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

       

        [AllowAnonymous]
        public ActionResult Manage(UserEditModel model)
        {

           
                int id = int.Parse((string)Session["LogedUserID"]);
                string username = Session["LogedUsername"].ToString();
                if (model.Id != 0)
                {
                    User v = db.Users.Where(x => x.Id == id && x.username.Equals(username) && x.Id == model.Id).FirstOrDefault();



                    if (v != null && v.password == model.password)
                    {
                        v.email = model.email;
                        v.password = model.newPassword != null ? model.newPassword : model.password;
                       
                        db.Entry(v).State = EntityState.Modified;
                   
                        db.SaveChanges();

                        model.username = v.username;
                        model.newPassword = "";
                        model.password = "";
                        model.confirmNewPassword = "";
                        ViewBag.Message = "Duomenys sėkmingai atnaujinti";
                        return View(model);

                    }
                    else
                    {
                        ViewBag.Error = "Neteisingas buvęs slaptažodis";
                        return View(model);
                    }
                }
                else
                {
                    var v = db.Users.Where(x => x.Id == id && x.username.Equals(username)).FirstOrDefault();

                    if (v != null)
                    {
                        model = new UserEditModel()
                        {
                            Id = v.Id,
                            username = v.username,
                            email = v.email   ,
                            oldPassword = v.password

                        };

                    }

                


            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["LogedUserID"] = null;
            Session["LogedUsername"] = null;
            Session["Permissions"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
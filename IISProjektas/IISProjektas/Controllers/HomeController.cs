﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IISProjektas.Models;

namespace IISProjektas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*List<Office> offices;
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            using (var db = new Entities())
            {
                offices = db.Offices.Where(x => x.Id != null).ToList();

            }
            ViewBag.off = offices;
            */






            return View();
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


            return View();
        }

        [HttpPost]
        public ActionResult CreateAdvertisement(AdvertisementCreateModel model)
        {



            return View();
        }

    }
}

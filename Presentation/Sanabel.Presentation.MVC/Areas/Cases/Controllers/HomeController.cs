﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Areas.Cases.Controllers
{
    public class HomeController : Controller
    {
        // GET: Cases/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
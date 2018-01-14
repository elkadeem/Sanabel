using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Areas.Volunteers.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        // GET: Volunteers/Default
        public string Index()
        {
            return "wael";
        }
    }
}
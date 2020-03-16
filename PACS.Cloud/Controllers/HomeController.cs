using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PACS.Cloud.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("viewer/{studyInstanceUid?}")]
        public ActionResult Viewer(string studyInstanceUid = "", string url = "")
        {
            return View();
        }
    }
}

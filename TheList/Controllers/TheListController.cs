using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheList.Controllers
{
    public class TheListController : Controller
    {
        // GET: TheList
        public ActionResult Index()
        {
            return View();
        }
    }
}
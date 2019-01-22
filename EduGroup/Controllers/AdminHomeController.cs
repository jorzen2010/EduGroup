using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduGroup.Controllers
{
    public class AdminHomeController : AdminBaseController
    {
        //
        // GET: /AdminHome/
        public ActionResult Index()
        {
            return View();
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkyCommon;
using SkyWechatService;

namespace EduGroup.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           WechatConfig config= AccessTokenService.GetWechatConfig();
           ViewData["config"] = config;
           return View();
        }

        public ActionResult About()
        {




            string xx = QRCodeTools.CreateQRCode("/","QRCode","Your application description page.");
            ViewBag.Message ="\\QRCode\\"+ xx;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Toupiao()
        {
            ViewBag.Message = "投票";

            return View();
        }
    }
}
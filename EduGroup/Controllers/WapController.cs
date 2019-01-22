using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using SkyDal;
using SkyEntity;
using SkyService;
using SkyCommon;

namespace EduGroup.Controllers
{
    public class WapController : Controller
    {
        //
        // GET: /Wap/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Menu2()
        {
            return View();
        }

        public ActionResult Menu3()
        {
            return View();
        }


         public ActionResult Pull(int? page)
        {

            Pager pager = new Pager();
            pager.table = "Jigou";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Jigou> dataList = DataConvertHelper<Jigou>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Jigou>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            ViewBag.pageno = pager.PageNo;
            return View(PageList);
        }

         public ActionResult PullNext(int? page)
         {

             Pager pager = new Pager();
             pager.table = "Jigou";
             pager.strwhere = "1=1";
             pager.PageSize = 2;
             pager.PageNo = page ?? 1;
             pager.FieldKey = "Id";
             pager.FiledOrder = "Id desc";

             pager = CommonDal.GetPager(pager);
             IList<Jigou> dataList = DataConvertHelper<Jigou>.ConvertToModel(pager.EntityDataTable);

             System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
             string strjson = js.Serialize(new { dataList = dataList});//将对象序列化成JSON字符串。匿名类。向浏览器返回多个JSON对象。  

             ////  string json = JsonHelper.JsonSerializerBySingleData(videoUrlfo);
             return Content(strjson);

  
             //return View(PageList);
         }


	}
}
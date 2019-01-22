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
    public class JigouController : AdminBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: /Jigou/
        public ActionResult Index(int? page)
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
            return View(PageList);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Jigou jigou)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.jigousRepository.Insert(jigou);
                unitOfWork.Save();
                return RedirectToAction("Index", "Jigou");
            }

            return View(jigou);
        }

        public ActionResult Edit(int id)
        {

            Jigou jigou = unitOfWork.jigousRepository.GetByID(id);

            if (jigou == null)
            {
                return HttpNotFound();
            }
            return View(jigou);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Jigou jigou)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.jigousRepository.Update(jigou);
                unitOfWork.Save();
                return RedirectToAction("Index", "Jigou");
            }
            return View(jigou);
        }

        //彻底删除
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int? id)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {

                unitOfWork.jigousRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}
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
    public class WebAdController : AdminBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: /Guanggao/
        public ActionResult Index(int? page)
        {

            Pager pager = new Pager();
            pager.table = "Guanggao";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<Guanggao> dataList = DataConvertHelper<Guanggao>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<Guanggao>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }

        public ActionResult Create()
        {
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(2);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Guanggao guanggao)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.guanggaosRepository.Insert(guanggao);
                unitOfWork.Save();
                return RedirectToAction("Index", "WebAd");
            }
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(2);
            return View(guanggao);
        }

        public ActionResult Edit(int id)
        {

            Guanggao guanggao = unitOfWork.guanggaosRepository.GetByID(id);
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(2);
            if (guanggao == null)
            {
                return HttpNotFound();
            }
            return View(guanggao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Guanggao guanggao)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.guanggaosRepository.Update(guanggao);
                unitOfWork.Save();
                return RedirectToAction("Index", "WebAd");
            }
            CategoryService cate = new CategoryService();
            ViewData["Categorylist"] = cate.GetCategorySelectList(2);
            return View(guanggao);
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

                unitOfWork.guanggaosRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
	}
}
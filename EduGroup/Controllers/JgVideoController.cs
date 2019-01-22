using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using PagedList;
using PagedList.Mvc;
using SkyDal;
using SkyEntity;
using SkyService;
using SkyCommon;
using AliyunVideo;

namespace EduGroup.Controllers
{
    public class JgVideoController : AdminBaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /JkSucai/
        public ActionResult Index(int? page)
        {

            Pager pager = new Pager();
            pager.table = "JgVideo";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "Id";
            pager.FiledOrder = "Id desc";

            pager = CommonDal.GetPager(pager);
            IList<JgVideo> dataList = DataConvertHelper<JgVideo>.ConvertToModel(pager.EntityDataTable);
            var PageList = new StaticPagedList<JgVideo>(dataList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(PageList);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(JgVideo jgVideo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.jgVideosRepository.Insert(jgVideo);
                unitOfWork.Save();
                return RedirectToAction("Index", "JgVideo");
            }

            return View(jgVideo);
        }

        public ActionResult Edit(int id)
        {

            JgVideo jgVideo = unitOfWork.jgVideosRepository.GetByID(id);

            if (jgVideo == null)
            {
                return HttpNotFound();
            }
            return View(jgVideo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(JgVideo jgVideo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.jgVideosRepository.Update(jgVideo);
                unitOfWork.Save();
                return RedirectToAction("Index", "JgVideo");
            }
            return View(jgVideo);
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

                unitOfWork.jgVideosRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewPlayAuth(int id)
        {
            JgVideo video = unitOfWork.jgVideosRepository.GetByID(id);

            if (video == null)
            {
                return HttpNotFound();
            }
           
                string ApiUrl = AliyunCommonParaConfig.ApiUrl;
                // 注意这里需要使用UTC时间，比北京时间少8小时。
                string Timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
                string Action = "GetVideoPlayAuth";
                string SignatureNonce = CommonTools.EncryptToSHA1(CommonTools.GenerateRandomNumber(8));

                //  string VideoId = "6ccf973fe06741e49ab849d4cec017e0";

                string VideoId = video.Content;
                ViewBag.VideoId = VideoId;

                ViewBag.PlayAuth = AliyunVideoServices.GetVideoInfo(ApiUrl, VideoId, Timestamp, Action, SignatureNonce).PlayAuth;
                ViewBag.title = "视频";
                return View(video);
        }

        public ActionResult ViewPlayUrl(int id)
        {
            JgVideo video = unitOfWork.jgVideosRepository.GetByID(id);


            string ApiUrl = AliyunCommonParaConfig.ApiUrl;
            // 注意这里需要使用UTC时间，比北京时间少8小时。
            string Timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
            string Action = "GetPlayInfo";
            string SignatureNonce = CommonTools.EncryptToSHA1(CommonTools.GenerateRandomNumber(8));


            string VideoId = video.Content;
            ViewBag.VideoId = VideoId;
            VideoUrlInfo videoUrlfo = new VideoUrlInfo();
            videoUrlfo = AliyunVideoServices.VideoUrlInfo(ApiUrl, VideoId, Timestamp, Action, SignatureNonce);

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strjson = js.Serialize(new { video = video, videoUrlfo = videoUrlfo });//将对象序列化成JSON字符串。匿名类。向浏览器返回多个JSON对象。  

            ////  string json = JsonHelper.JsonSerializerBySingleData(videoUrlfo);
            //return Content(strjson);

            ViewBag.playurl =videoUrlfo.PlayInfoList.PlayInfo[1].PlayURL;
            ViewBag.strjson = strjson;
            return View(video);



        }


	}
}
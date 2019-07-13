using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
using static WebApplication1.Common.General;

namespace WebApplication1.Controllers
{
    public class NewsController : BaseController
    {
        private WebApplication1Entities db = new WebApplication1Entities();
        private static int _size = 8;
        // GET: News
        public ActionResult Index(int? page)
        {
            DateTime ngayhientai = DateTime.Now;
            ViewBag.ngayhientai = ngayhientai;
            ViewBag.namhientai = DateTime.Now.Year;

            List<admin_tintuc> tintucArr = db.admin_tintuc.Where(s => s.hienthi==true && s.daxoa == false).ToList();
            return View(tintucArr);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_tintuc admin_tintuc = db.admin_tintuc.Find(id);
            if (admin_tintuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.hinhdaidien = @"../.." + admin_tintuc.hinhdaidien;

            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Tintuc && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s => s.urlbanner).FirstOrDefault();
            ViewBag.arrBanner = "../.." + arr_imgBanner;
            return View(admin_tintuc);
        }
    }
}
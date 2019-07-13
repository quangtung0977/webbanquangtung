using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
using static WebApplication1.Common.General;

namespace WebApplication1.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities(); //goi database
        public class HomeViewModel
        {
            public List<sanpham> sanphamArr { get; set; }
            public List<loaisanpham> loaisanphamArr { get; set; }
        }
        public ActionResult Index()
        {
            HomeViewModel homeArr = new HomeViewModel();
            homeArr.loaisanphamArr = db.loaisanphams.Where(s => s.hienthi == true).ToList();
            homeArr.sanphamArr = db.sanphams.Where(s => s.hienthi == true).ToList();
            return View(homeArr);
        }

        public ActionResult About()
        {
            // Trang hoi dap
            return View();
        }

        public ActionResult Contact()
        {
            //trang lien he
            return View();
        }
    }
}
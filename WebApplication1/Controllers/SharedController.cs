using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Controllers
{
    public class SharedController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();
        // GET: Shared
        public ActionResult Footer_()
        {
            try
            {
                admin_thongtincodinh rs = db.admin_thongtincodinh.Find(-1);
                ViewBag.thongtincodinh_diachi = rs.diachi;
                ViewBag.thongtincodinh_dienthoai = rs.dienthoai;
                ViewBag.thongtincodinh_mail = rs.mail;

                if (Session["CurrentCulture"] != null)
                {
                    if (Session["CurrentCulture"].ToString() == "en-US")
                    {
                        ViewBag.thongtincodinh_diachi = rs.diachi_en;
                    }
                }
                return PartialView("Footer_");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult _Header()
        {
            try
            {
                List<loaisanpham> loaisanphamArr = new List<loaisanpham>();
                loaisanphamArr = db.loaisanphams.Where(s => s.hienthi == true).ToList();
                return PartialView("_Header", loaisanphamArr);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
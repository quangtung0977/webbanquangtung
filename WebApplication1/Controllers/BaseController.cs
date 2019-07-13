using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class BaseController:Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContent)
        {
            base.Initialize(requestContent);
            if (Session["CurrentCulture"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["CurrentCulture"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CurrentCulture"].ToString());
            }
            else
            {
                Session["CurrentCulture"] = "vi-VN";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
            }
            
        }
        public ActionResult ChangeCulture(string ddCul,string url)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddCul);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddCul);
            //url Home;Index
            var arr = url.Split(';');
            Session["CurrentCulture"] = ddCul;

            Session["Thanhvien"] = "Thành viên";
            Session["Gioithieu"] = "Giới thiệu";
            Session["Hanhtrinh"] = "Hành trình";
            Session["Tintucmoi"] = "Tin tức mới";
            Session["Hinhanh"] = "Hình ảnh";
            Session["Lienhe"] = "Liên hệ";

            Session["Hanhtrinhct"] = "Chi tiết hành trình";
            Session["Tintucmoict"] = "Chi tiết tin tức";
            Session["Hinhanhct"] = "Chi tiết hình ảnh";
            Session["Thanhvienct"] = "Chi tiết thành viên";

            Session["Diachicongty"] = "Địa chỉ công ty";
            Session["Dienthoailienlac"] = "Điện thoại liên lạc";
            Session["Trangchu"] = "Trang chủ";


            if (ddCul == "en-US"){
                Session["Thanhvien"] = "Members";
                Session["Gioithieu"] = "About us";
                Session["Hanhtrinh"] = "Journeys";
                Session["Tintucmoi"] = "News";
                Session["Hinhanh"] = "Galleries";
                Session["Lienhe"] = "Contact";

                Session["Hanhtrinhct"] = "Journey Details";
                Session["Tintucmoict"] = "News Details";
                Session["Hinhanhct"] = "Gallery Details";
                Session["Thanhvienct"] = "Member Details";

                Session["Diachicongty"] = "Offical Location";
                Session["Dienthoailienlac"] = "Phone number";
                Session["Trangchu"] = "Home";
            }

            return RedirectToAction(arr[1], arr[0]);
        }
    }
}
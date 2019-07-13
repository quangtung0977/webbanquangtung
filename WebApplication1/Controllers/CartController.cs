using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebApplication1.Controllers.ProductsController;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Purchase()
        {
            List<SanPhamGioHang> listSPGH = new List<SanPhamGioHang>();
            if (Session["GioHang"] != null)
            {
                listSPGH = Session["GioHang"] as List<SanPhamGioHang>;
            }
            int tongtien = 0;
            if (listSPGH != null)
            {
                foreach(var item in listSPGH)
                {
                    int thanhtien = item.gia * item.soluong;
                    tongtien = tongtien + thanhtien;
                }
            }
            ViewBag.tongtien = tongtien;
            return View(listSPGH);
        }
        [HttpPost]
        public ActionResult XoaSanPhamGioHang(string idsp)
        {
            List<SanPhamGioHang> listSPGH = Session["GioHang"] as List<SanPhamGioHang>; ;
            if (listSPGH != null)
            {
                SanPhamGioHang spgiohang = listSPGH.FirstOrDefault(s => s.id == idsp);
                if (spgiohang != null)
                {
                    if (spgiohang.soluong > 1)
                    {
                        spgiohang.soluong--;
                    }else
                    {
                        listSPGH.Remove(spgiohang);
                    }
                }
                Session["GioHang"] = listSPGH;
            }
            return RedirectToAction("Purchase", "Cart");
        }
        [HttpPost]
        public ActionResult TienHanhDatHang()
        {

            return RedirectToAction("Purchase", "Cart");
        }
    }
}
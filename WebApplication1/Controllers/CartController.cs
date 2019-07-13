using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
using static WebApplication1.Controllers.ProductsController;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        
        private WebApplication1Entities db = new WebApplication1Entities(); //goi database
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
        public ActionResult TienHanhDatHang(string tenKH, string diachi, string sodienthoai, string ghichu)
        {
            try
            {
                List<SanPhamGioHang> listSPGH = Session["GioHang"] as List<SanPhamGioHang>; ;
                if (listSPGH == null)
                {
                    return RedirectToAction("Purchase", "Cart");
                }
                else
                {
                    banhang objBanhang = new banhang();
                    int tongtien = 0;
                    if (listSPGH != null)
                    {
                        foreach (var item in listSPGH)
                        {
                            int thanhtien = item.gia * item.soluong;
                            tongtien = tongtien + thanhtien;
                        }
                    }
                    objBanhang.tongtien = tongtien;
                    objBanhang.tenKH = tenKH;
                    objBanhang.diachiKH = diachi;
                    objBanhang.dienthoaiKH = sodienthoai;
                    objBanhang.ghichu = ghichu;
                    objBanhang.ngaydathang = DateTime.Now;
                    objBanhang.ngaycapnhat = DateTime.Now;
                    objBanhang.trangthaidonhang = Convert.ToInt32(TrangThaiDonHang.DonHangmoi);

                    try
                    {
                        db.banhangs.Add(objBanhang);
                        db.SaveChanges();

                        foreach (var item in listSPGH)
                        {
                            chitietdonhang objCTDH = new chitietdonhang();
                            objCTDH.idsp = item.id;
                            objCTDH.soluong = item.soluong;
                            objCTDH.giaban = item.gia;
                            objCTDH.iddonbanhang = objBanhang.id;
                            try
                            {
                                db.chitietdonhangs.Add(objCTDH);
                                db.SaveChanges();
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
                return RedirectToAction("Index", "XacNhanDonHangThanhCong");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public ActionResult GiamSoLuong(string idsp_giam)
        {
            List<SanPhamGioHang> listSPGH = Session["GioHang"] as List<SanPhamGioHang>; ;
            if (listSPGH != null)
            {
                SanPhamGioHang spgiohang = listSPGH.FirstOrDefault(s => s.id == idsp_giam);
                if (spgiohang != null)
                {
                    if (spgiohang.soluong > 1)
                    {
                        spgiohang.soluong--;
                    }
                    else
                    {
                        listSPGH.Remove(spgiohang);
                    }
                }
                Session["GioHang"] = listSPGH;
            }
            return RedirectToAction("Purchase", "Cart");
        }
        [HttpPost]
        public ActionResult TangSoLuong(string idsp_tang)
        {
            List<SanPhamGioHang> listSPGH = Session["GioHang"] as List<SanPhamGioHang>; ;
            if (listSPGH != null)
            {
                SanPhamGioHang spgiohang = listSPGH.FirstOrDefault(s => s.id == idsp_tang);
                if (spgiohang != null)
                {
                    spgiohang.soluong++;
                }
                Session["GioHang"] = listSPGH;
            }
            return RedirectToAction("Purchase", "Cart");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : BaseController
    {
        public class SanPhamGioHang
        {
            public string id { get; set; }
            public string ten { get; set; }
            public int gia { get; set; }
            public int soluong { get; set; }
            public string urlhinh { get; set; }
        }
        private WebApplication1Entities db = new WebApplication1Entities();
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<sanpham> sanphamtheoloaiArr = db.sanphams.Where(s => s.loaisanpham1.id == id).ToList();
            return View(sanphamtheoloaiArr);
        }
        public ActionResult Detail(string id)
        {
            sanpham productbyid = db.sanphams.Where(s => s.id ==id).FirstOrDefault();
            
            if (productbyid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idsanpham = id;
            return View(productbyid);
        }
        [HttpPost]
        public ActionResult ThemGioHang(string idsp)
        {
            sanpham objSp = db.sanphams.Find(idsp);
            if (Session["GioHang"] == null)
            {
                SanPhamGioHang spgh = new SanPhamGioHang();
                int value;
                spgh.id = objSp.id;
                spgh.gia = 0;
                spgh.urlhinh = objSp.hinhdaidien;
                try
                {
                    spgh.gia = Convert.ToInt32(objSp.giaban);
                }
                catch { }
                spgh.soluong = 1;
                spgh.ten = objSp.ten;
                List<SanPhamGioHang> listSPGH = new List<SanPhamGioHang>();
                listSPGH.Add(spgh);
                Session["GioHang"] = listSPGH;
            }
            else
            {
                //Kiem tra hang vua them da ton tai chua (de cap nhat so luong), neu chua, them mon do vao gio hang
                List<SanPhamGioHang> listSPGH = Session["GioHang"] as List<SanPhamGioHang>;
                SanPhamGioHang spthem = listSPGH.FirstOrDefault(s=>s.id == idsp);
                if (spthem == null)
                {
                    SanPhamGioHang spgh = new SanPhamGioHang();
                    int value;
                    spgh.id = objSp.id;
                    spgh.gia = 0;
                    spgh.urlhinh = objSp.hinhdaidien;
                    try
                    {
                        spgh.gia = Convert.ToInt32(objSp.giaban);
                    }
                    catch { }
                    spgh.soluong = 1;
                    spgh.ten = objSp.ten;
                    listSPGH.Add(spgh);
                }
                else
                {
                    spthem.soluong++;
                }
                Session["GioHang"] = listSPGH;
            }
            return RedirectToAction("Purchase", "Cart");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var sanphams = db.sanphams.Include(s => s.loaisanpham1);
            return View(sanphams.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.loaisanpham = new SelectList(db.loaisanphams, "id", "ten");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,ten,loaisanpham,ngaytao,ngaycapnhat,nguoitao,motasoluoc,motachitiet,daxoa,ghichu,hienthi,hinhdaidien,hinhanhsanpham,thutuhien,gianiemyet,giaban,mathuonghieu")] sanpham sanpham, string[] addmore)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    admin_account u = Session["User"] as admin_account;
                    sanpham.nguoitao = u.id;
                    sanpham.daxoa = false;
                    sanpham.ngaytao = DateTime.Now;
                    sanpham.ngaycapnhat = DateTime.Now;
                    db.sanphams.Add(sanpham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.loaisanpham = new SelectList(db.loaisanphams, "id", "ten", sanpham.loaisanpham);
                return View(sanpham);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.loaisanpham = new SelectList(db.loaisanphams, "id", "ten", sanpham.loaisanpham);
            return View(sanpham);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,ten,loaisanpham,ngaytao,ngaycapnhat,nguoitao,motasoluoc,motachitiet,daxoa,ghichu,hienthi,hinhdaidien,hinhanhsanpham,thutuhien,gianiemyet,giaban,mathuonghieu")] sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                sanpham.ngaycapnhat = DateTime.Now;
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.loaisanpham = new SelectList(db.loaisanphams, "id", "ten", sanpham.loaisanpham);
            return View(sanpham);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = db.sanphams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            sanpham sanpham = db.sanphams.Find(id);
            sanpham.daxoa = true;
            //db.sanphams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    public class HinhAnhController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/HinhAnh
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_hinhanh = db.admin_hinhanh.Include(a => a.admin_account).Include(a => a.admin_loaihinhanh).Where(s => s.daxoa != true);
            return View(admin_hinhanh.ToList());
        }

        // GET: Admin/HinhAnh/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_hinhanh admin_hinhanh = db.admin_hinhanh.Find(id);
            if (admin_hinhanh == null)
            {
                return HttpNotFound();
            }
            return View(admin_hinhanh);
        }

        // GET: Admin/HinhAnh/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            ViewBag.loaihinhanh = new SelectList(db.admin_loaihinhanh, "id", "tenloaihinhanh");
            return View();
        }

        // POST: Admin/HinhAnh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,tenhinhanh,urlhinhanh,ngaytao,ngaycapnhat,nguoitao,loaihinhanh,thutuhien,daxoa,ghichu")] admin_hinhanh admin_hinhanh)
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Logout", "Account");
                }
                if (ModelState.IsValid)
                {
                    if (Session["User"] != null)
                    {
                        admin_account u = Session["User"] as admin_account;
                        admin_hinhanh.nguoitao = u.id;
                    }
                    admin_hinhanh.ngaytao = DateTime.Now;
                    admin_hinhanh.ngaycapnhat = DateTime.Now;
                    db.admin_hinhanh.Add(admin_hinhanh);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_hinhanh.nguoitao);
                ViewBag.loaihinhanh = new SelectList(db.admin_loaihinhanh, "id", "tenloaihinhanh", admin_hinhanh.loaihinhanh);
                return View(admin_hinhanh);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: Admin/HinhAnh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_hinhanh admin_hinhanh = db.admin_hinhanh.Find(id);
            if (admin_hinhanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_hinhanh.nguoitao);
            ViewBag.loaihinhanh = new SelectList(db.admin_loaihinhanh, "id", "tenloaihinhanh", admin_hinhanh.loaihinhanh);

            ViewBag._hinhdaidien = "../../.." + admin_hinhanh.urlhinhanh;
            return View(admin_hinhanh);
        }

        // POST: Admin/HinhAnh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,tenhinhanh,urlhinhanh,ngaytao,ngaycapnhat,nguoitao,loaihinhanh,thutuhien,daxoa,ghichu")] admin_hinhanh admin_hinhanh)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_hinhanh.ngaycapnhat = DateTime.Now;
                db.Entry(admin_hinhanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_hinhanh.nguoitao);
            ViewBag.loaihinhanh = new SelectList(db.admin_loaihinhanh, "id", "tenloaihinhanh", admin_hinhanh.loaihinhanh);

            ViewBag._hinhdaidien ="../../"+ admin_hinhanh.urlhinhanh;
            return View(admin_hinhanh);
        }

        // GET: Admin/HinhAnh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_hinhanh admin_hinhanh = db.admin_hinhanh.Find(id);
            if (admin_hinhanh == null)
            {
                return HttpNotFound();
            }
            return View(admin_hinhanh);
        }

        // POST: Admin/HinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_hinhanh admin_hinhanh = db.admin_hinhanh.Find(id);
            //db.admin_hinhanh.Remove(admin_hinhanh);
            admin_hinhanh.ngaycapnhat = DateTime.Now;
            admin_hinhanh.daxoa = true;
            db.Entry(admin_hinhanh).State = EntityState.Modified;
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

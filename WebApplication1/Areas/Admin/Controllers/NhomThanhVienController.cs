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
    public class NhomThanhVienController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/NhomThanhVien
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_nhomthanhvien = db.admin_nhomthanhvien.Include(a => a.admin_account).Where(s => s.daxoa != true && s.id != -1);
            return View(admin_nhomthanhvien.ToList());
        }

        // GET: Admin/NhomThanhVien/Details/5
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
            admin_nhomthanhvien admin_nhomthanhvien = db.admin_nhomthanhvien.Find(id);
            if (admin_nhomthanhvien == null)
            {
                return HttpNotFound();
            }
            return View(admin_nhomthanhvien);
        }

        // GET: Admin/NhomThanhVien/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            return View();
        }

        // POST: Admin/NhomThanhVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tennhomthanhvien,motanhom,ngaytao,ngaycapnhat,nguoitao,daxoa,ghichu,hinhdaidien")] admin_nhomthanhvien admin_nhomthanhvien)
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
                    admin_nhomthanhvien.nguoitao = u.id;
                }
                admin_nhomthanhvien.ngaytao = DateTime.Now;
                admin_nhomthanhvien.ngaycapnhat = DateTime.Now;
                db.admin_nhomthanhvien.Add(admin_nhomthanhvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_nhomthanhvien.nguoitao);
            return View(admin_nhomthanhvien);
        }

        // GET: Admin/NhomThanhVien/Edit/5
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
            admin_nhomthanhvien admin_nhomthanhvien = db.admin_nhomthanhvien.Find(id);
            if (admin_nhomthanhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_nhomthanhvien.nguoitao);
            return View(admin_nhomthanhvien);
        }

        // POST: Admin/NhomThanhVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tennhomthanhvien,motanhom,ngaytao,ngaycapnhat,nguoitao,daxoa,ghichu,hinhdaidien")] admin_nhomthanhvien admin_nhomthanhvien)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_nhomthanhvien.ngaycapnhat = DateTime.Now;
                db.Entry(admin_nhomthanhvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_nhomthanhvien.nguoitao);
            return View(admin_nhomthanhvien);
        }

        // GET: Admin/NhomThanhVien/Delete/5
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
            admin_nhomthanhvien admin_nhomthanhvien = db.admin_nhomthanhvien.Find(id);
            if (admin_nhomthanhvien == null)
            {
                return HttpNotFound();
            }
            return View(admin_nhomthanhvien);
        }

        // POST: Admin/NhomThanhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_nhomthanhvien admin_nhomthanhvien = db.admin_nhomthanhvien.Find(id);
            //db.admin_nhomthanhvien.Remove(admin_nhomthanhvien);
            admin_nhomthanhvien.ngaycapnhat = DateTime.Now;
            admin_nhomthanhvien.daxoa = true;
            db.Entry(admin_nhomthanhvien).State = EntityState.Modified;
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

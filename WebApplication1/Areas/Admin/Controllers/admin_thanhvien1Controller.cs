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
    public class admin_thanhvien1Controller : Controller
    {
        private WebApplication1Entities1 db = new WebApplication1Entities1();

        // GET: Admin/admin_thanhvien1
        public ActionResult Index()
        {
            var admin_thanhvien = db.admin_thanhvien.Include(a => a.admin_account).Include(a => a.admin_nhomthanhvien);
            return View(admin_thanhvien.ToList());
        }

        // GET: Admin/admin_thanhvien1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            if (admin_thanhvien == null)
            {
                return HttpNotFound();
            }
            return View(admin_thanhvien);
        }

        // GET: Admin/admin_thanhvien1/Create
        public ActionResult Create()
        {
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien");
            return View();
        }

        // POST: Admin/admin_thanhvien1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenthanhvien,motasoluoc,chitiet,urlhinhanh,ngaytao,ngaycapnhat,nguoitao,daxoa,ghichu,nhomthanhvien,hienthi,tenthanhvien_en,motasoluoc_en,chitiet_en,thutuhienthi")] admin_thanhvien admin_thanhvien)
        {
            if (ModelState.IsValid)
            {
                db.admin_thanhvien.Add(admin_thanhvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_thanhvien.nguoitao);
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien", admin_thanhvien.nhomthanhvien);
            return View(admin_thanhvien);
        }

        // GET: Admin/admin_thanhvien1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            if (admin_thanhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_thanhvien.nguoitao);
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien", admin_thanhvien.nhomthanhvien);
            return View(admin_thanhvien);
        }

        // POST: Admin/admin_thanhvien1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tenthanhvien,motasoluoc,chitiet,urlhinhanh,ngaytao,ngaycapnhat,nguoitao,daxoa,ghichu,nhomthanhvien,hienthi,tenthanhvien_en,motasoluoc_en,chitiet_en,thutuhienthi")] admin_thanhvien admin_thanhvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_thanhvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_thanhvien.nguoitao);
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien", admin_thanhvien.nhomthanhvien);
            return View(admin_thanhvien);
        }

        // GET: Admin/admin_thanhvien1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            if (admin_thanhvien == null)
            {
                return HttpNotFound();
            }
            return View(admin_thanhvien);
        }

        // POST: Admin/admin_thanhvien1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            db.admin_thanhvien.Remove(admin_thanhvien);
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

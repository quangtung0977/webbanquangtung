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
    public class LoaiHanhTrinhController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/LoaiHanhTrinh
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_loaihanhtrinh = db.admin_loaihanhtrinh.Include(a => a.admin_account).Where(s => s.daxoa != true  && s.id !=-1);
            return View(admin_loaihanhtrinh.ToList());
        }

        // GET: Admin/LoaiHanhTrinh/Details/5
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
            admin_loaihanhtrinh admin_loaihanhtrinh = db.admin_loaihanhtrinh.Find(id);
            if (admin_loaihanhtrinh == null)
            {
                return HttpNotFound();
            }
            return View(admin_loaihanhtrinh);
        }

        // GET: Admin/LoaiHanhTrinh/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            return View();
        }

        // POST: Admin/LoaiHanhTrinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenloaihanhtrinh,ngaytao,ngaycapnhat,hienthi,nguoitao")] admin_loaihanhtrinh admin_loaihanhtrinh)
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
                    admin_loaihanhtrinh.nguoitao = u.id;
                }
                admin_loaihanhtrinh.ngaytao = DateTime.Now;
                admin_loaihanhtrinh.ngaycapnhat = DateTime.Now;
                db.admin_loaihanhtrinh.Add(admin_loaihanhtrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_loaihanhtrinh.nguoitao);
            return View(admin_loaihanhtrinh);
        }

        // GET: Admin/LoaiHanhTrinh/Edit/5
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
            admin_loaihanhtrinh admin_loaihanhtrinh = db.admin_loaihanhtrinh.Find(id);
            if (admin_loaihanhtrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_loaihanhtrinh.nguoitao);
            return View(admin_loaihanhtrinh);
        }

        // POST: Admin/LoaiHanhTrinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tenloaihanhtrinh,ngaytao,ngaycapnhat,hienthi,nguoitao")] admin_loaihanhtrinh admin_loaihanhtrinh)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_loaihanhtrinh.ngaycapnhat = DateTime.Now;
                db.Entry(admin_loaihanhtrinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_loaihanhtrinh.nguoitao);
            return View(admin_loaihanhtrinh);
        }

        // GET: Admin/LoaiHanhTrinh/Delete/5
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
            admin_loaihanhtrinh admin_loaihanhtrinh = db.admin_loaihanhtrinh.Find(id);
            if (admin_loaihanhtrinh == null)
            {
                return HttpNotFound();
            }
            return View(admin_loaihanhtrinh);
        }

        // POST: Admin/LoaiHanhTrinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_loaihanhtrinh admin_loaihanhtrinh = db.admin_loaihanhtrinh.Find(id);
            //db.admin_loaihanhtrinh.Remove(admin_loaihanhtrinh);
            admin_loaihanhtrinh.ngaycapnhat = DateTime.Now;
            admin_loaihanhtrinh.daxoa = true;
            db.Entry(admin_loaihanhtrinh).State = EntityState.Modified;
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

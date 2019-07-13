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
    public class bannersController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/banners
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var banners = db.banners.Include(b => b.admin_account).Include(b => b.loaibanner1)
                .Where(s => s.daxoa != true).OrderBy(s=>s.loaibanner1.id);
            return View(banners.ToList());
        }

        // GET: Admin/banners/Details/5
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
            banner banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Admin/banners/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            ViewBag.loaibanner = new SelectList(db.loaibanners, "id", "tenloaibanner");
            return View();
        }

        // POST: Admin/banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenbanner,urlbanner,motabanner,ghichu,ngaytao,ngaycapnhat,nguoitao,hienthi,thutuhien,daxoa,loaibanner")] banner banner)
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
                    banner.nguoitao = u.id;
                }
                banner.ngaytao = DateTime.Now;
                banner.ngaycapnhat = DateTime.Now;
                db.banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", banner.nguoitao);
            ViewBag.loaibanner = new SelectList(db.loaibanners, "id", "tenloaibanner", banner.loaibanner);
            return View(banner);
        }

        // GET: Admin/banners/Edit/5
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
            banner banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", banner.nguoitao);
            ViewBag.loaibanner = new SelectList(db.loaibanners, "id", "tenloaibanner", banner.loaibanner);
            return View(banner);
        }

        // POST: Admin/banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tenbanner,urlbanner,motabanner,ghichu,ngaytao,ngaycapnhat,nguoitao,hienthi,thutuhien,daxoa,loaibanner")] banner banner)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                banner.ngaycapnhat = DateTime.Now;
                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", banner.nguoitao);
            ViewBag.loaibanner = new SelectList(db.loaibanners, "id", "tenloaibanner", banner.loaibanner);
            return View(banner);
        }

        // GET: Admin/banners/Delete/5
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
            banner banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            banner banner = db.banners.Find(id);
            //db.banners.Remove(banner);
            banner.ngaycapnhat = DateTime.Now;
            banner.daxoa = true;
            db.Entry(banner).State = EntityState.Modified;
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

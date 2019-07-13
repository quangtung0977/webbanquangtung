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
    public class LoaiTinTucController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/LoaiTinTuc
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_loaitintuc = db.admin_loaitintuc.Include(a => a.admin_account).Where(s => s.daxoa != true && s.id != -1);
            return View(admin_loaitintuc.ToList());
        }

        // GET: Admin/LoaiTinTuc/Details/5
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
            admin_loaitintuc admin_loaitintuc = db.admin_loaitintuc.Find(id);
            if (admin_loaitintuc == null)
            {
                return HttpNotFound();
            }
            return View(admin_loaitintuc);
        }

        // GET: Admin/LoaiTinTuc/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            return View();
        }

        // POST: Admin/LoaiTinTuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenloaitintuc,ngaytao,ngaycapnhat,nguoitao,daxoa,hinhanhtintuc,motasoluoc,ghichu")] admin_loaitintuc admin_loaitintuc)
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
                    admin_loaitintuc.nguoitao = u.id;
                }
                admin_loaitintuc.ngaytao = DateTime.Now;
                admin_loaitintuc.ngaycapnhat = DateTime.Now;
                db.admin_loaitintuc.Add(admin_loaitintuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_loaitintuc);
        }

        // GET: Admin/LoaiTinTuc/Edit/5
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
            admin_loaitintuc admin_loaitintuc = db.admin_loaitintuc.Find(id);
            if (admin_loaitintuc == null)
            {
                return HttpNotFound();
            }
            return View(admin_loaitintuc);
        }

        // POST: Admin/LoaiTinTuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tenloaitintuc,ngaytao,ngaycapnhat,nguoitao,daxoa,hinhanhtintuc,motasoluoc,ghichu")] admin_loaitintuc admin_loaitintuc)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_loaitintuc.ngaycapnhat = DateTime.Now;
                db.Entry(admin_loaitintuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_loaitintuc);
        }

        // GET: Admin/LoaiTinTuc/Delete/5
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
            admin_loaitintuc admin_loaitintuc = db.admin_loaitintuc.Find(id);
            if (admin_loaitintuc == null)
            {
                return HttpNotFound();
            }
            return View(admin_loaitintuc);
        }

        // POST: Admin/LoaiTinTuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_loaitintuc admin_loaitintuc = db.admin_loaitintuc.Find(id);
            //db.admin_loaitintuc.Remove(admin_loaitintuc);
            admin_loaitintuc.ngaycapnhat = DateTime.Now;
            admin_loaitintuc.daxoa = true;
            db.Entry(admin_loaitintuc).State = EntityState.Modified;
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

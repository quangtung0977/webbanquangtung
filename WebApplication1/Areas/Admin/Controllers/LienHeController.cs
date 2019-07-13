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
    public class LienHeController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/LienHe
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_baivietlienhe = db.admin_baivietlienhe.Include(a => a.admin_account).Where(s => s.daxoa != true);
            return View(admin_baivietlienhe.ToList());
        }

        // GET: Admin/LienHe/Details/5
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
            admin_baivietlienhe admin_baivietlienhe = db.admin_baivietlienhe.Find(id);
            if (admin_baivietlienhe == null)
            {
                return HttpNotFound();
            }
            return View(admin_baivietlienhe);
        }

        // GET: Admin/LienHe/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            return View();
        }

        // POST: Admin/LienHe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,tenbailienhe,tenbailienhe_en,noidung,noidung_en,hienthi,daxoa,ngaytao,ngaycapnhat,nguoitao,thutuhienthi,ghichu")] admin_baivietlienhe admin_baivietlienhe)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_baivietlienhe.ngaytao = DateTime.Now;
                admin_baivietlienhe.ngaycapnhat = DateTime.Now;
                if (Session["User"] != null)
                {
                    admin_account u = Session["User"] as admin_account;
                    admin_baivietlienhe.nguoitao = u.id;
                }
                db.admin_baivietlienhe.Add(admin_baivietlienhe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_baivietlienhe.nguoitao);
            return View(admin_baivietlienhe);
        }

        // GET: Admin/LienHe/Edit/5
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
            admin_baivietlienhe admin_baivietlienhe = db.admin_baivietlienhe.Find(id);
            if (admin_baivietlienhe == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_baivietlienhe.nguoitao);
            return View(admin_baivietlienhe);
        }

        // POST: Admin/LienHe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,tenbailienhe,tenbailienhe_en,noidung,noidung_en,hienthi,daxoa,ngaytao,ngaycapnhat,nguoitao,thutuhienthi,ghichu")] admin_baivietlienhe admin_baivietlienhe)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Entry(admin_baivietlienhe).State = EntityState.Modified;
                admin_baivietlienhe.ngaycapnhat = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_baivietlienhe.nguoitao);
            return View(admin_baivietlienhe);
        }

        // GET: Admin/LienHe/Delete/5
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
            admin_baivietlienhe admin_baivietlienhe = db.admin_baivietlienhe.Find(id);
            if (admin_baivietlienhe == null)
            {
                return HttpNotFound();
            }
            return View(admin_baivietlienhe);
        }

        // POST: Admin/LienHe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_baivietlienhe admin_baivietlienhe = db.admin_baivietlienhe.Find(id);
            //db.admin_baivietlienhe.Remove(admin_baivietlienhe);
            admin_baivietlienhe.ngaycapnhat = DateTime.Now;
            admin_baivietlienhe.daxoa = true;
            db.Entry(admin_baivietlienhe).State = EntityState.Modified;
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

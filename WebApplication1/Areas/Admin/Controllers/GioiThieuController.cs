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
    public class GioiThieuController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/GioiThieu
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_gioithieu = db.admin_gioithieu.Include(a => a.admin_account).Where(s=>s.daxoa!=true);
            return View(admin_gioithieu.ToList());
        }

        // GET: Admin/GioiThieu/Details/5
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
            admin_gioithieu admin_gioithieu = db.admin_gioithieu.Find(id);
            if (admin_gioithieu == null)
            {
                return HttpNotFound();
            }
            return View(admin_gioithieu);
        }

        // GET: Admin/GioiThieu/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            return View();
        }

        // POST: Admin/GioiThieu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,tenbaigioithieu,tenbaigioithieu_en,noidung,noidung_en,hienthi,thutuhienthi,daxoa,nguoitao,ngaytao,ngaycapnhat,ghichu")] admin_gioithieu admin_gioithieu)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    admin_gioithieu.ngaytao = DateTime.Now;
                    admin_gioithieu.ngaycapnhat = DateTime.Now;
                    if (Session["User"] != null)
                    {
                        admin_account u = Session["User"] as admin_account;
                        admin_gioithieu.nguoitao = u.id;
                    }
                    db.admin_gioithieu.Add(admin_gioithieu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_gioithieu.nguoitao);
                return View(admin_gioithieu);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: Admin/GioiThieu/Edit/5
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
            admin_gioithieu admin_gioithieu = db.admin_gioithieu.Find(id);
            if (admin_gioithieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_gioithieu.nguoitao);
            return View(admin_gioithieu);
        }

        // POST: Admin/GioiThieu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,tenbaigioithieu,tenbaigioithieu_en,noidung,noidung_en,hienthi,thutuhienthi,daxoa,nguoitao,ngaytao,ngaycapnhat,ghichu")] admin_gioithieu admin_gioithieu)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_gioithieu.ngaycapnhat = DateTime.Now;
                db.Entry(admin_gioithieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_gioithieu.nguoitao);
            return View(admin_gioithieu);
        }

        // GET: Admin/GioiThieu/Delete/5
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
            admin_gioithieu admin_gioithieu = db.admin_gioithieu.Find(id);
            if (admin_gioithieu == null)
            {
                return HttpNotFound();
            }
            return View(admin_gioithieu);
        }

        // POST: Admin/GioiThieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_gioithieu admin_gioithieu = db.admin_gioithieu.Find(id);
            //db.admin_gioithieu.Remove(admin_gioithieu);
            admin_gioithieu.daxoa = true;
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

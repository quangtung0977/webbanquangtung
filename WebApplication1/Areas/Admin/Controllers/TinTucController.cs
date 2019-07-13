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
    public class TinTucController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/TinTuc
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_tintuc = db.admin_tintuc.Include(a => a.admin_account).Include(a => a.admin_loaitintuc).Where(s=>s.daxoa!=true);
            return View(admin_tintuc.ToList());
        }

        // GET: Admin/TinTuc/Details/5
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
            admin_tintuc admin_tintuc = db.admin_tintuc.Find(id);
            if (admin_tintuc == null)
            {
                return HttpNotFound();
            }
            return View(admin_tintuc);
        }

        // GET: Admin/TinTuc/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            ViewBag.loaitintuc = new SelectList(db.admin_loaitintuc, "id", "tenloaitintuc");
            return View();
        }

        // POST: Admin/TinTuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,tentintuc,tentintuc_en,hinhdaidien,loaitintuc,ngaytao,ngaycapnhat,nguoitao,motasoluoc,motasoluoc_en,noidungtintuc,noidungtintuc_en,daxoa,ghichu,hienthi")] admin_tintuc admin_tintuc)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    admin_tintuc.ngaytao = DateTime.Now;
                    admin_tintuc.ngaycapnhat = DateTime.Now;
                    admin_tintuc.daxoa = false;
                    if (Session["User"] != null)
                    {
                        admin_account u = Session["User"] as admin_account;
                        admin_tintuc.nguoitao = u.id;
                    }
                    db.admin_tintuc.Add(admin_tintuc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_tintuc.nguoitao);
                ViewBag.loaitintuc = new SelectList(db.admin_loaitintuc, "id", "tenloaitintuc", admin_tintuc.loaitintuc);
                return View(admin_tintuc);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: Admin/TinTuc/Edit/5
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
            admin_tintuc admin_tintuc = db.admin_tintuc.Find(id);
            if (admin_tintuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_tintuc.nguoitao);
            ViewBag.loaitintuc = new SelectList(db.admin_loaitintuc, "id", "tenloaitintuc", admin_tintuc.loaitintuc);
            return View(admin_tintuc);
        }

        // POST: Admin/TinTuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,tentintuc,tentintuc_en,hinhdaidien,loaitintuc,ngaytao,ngaycapnhat,nguoitao,motasoluoc,motasoluoc_en,noidungtintuc,noidungtintuc_en,daxoa,ghichu,hienthi")] admin_tintuc admin_tintuc)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                //admin_tintuc.ngaycapnhat = DateTime.Now;
                db.Entry(admin_tintuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_tintuc.nguoitao);
            ViewBag.loaitintuc = new SelectList(db.admin_loaitintuc, "id", "tenloaitintuc", admin_tintuc.loaitintuc);
            return View(admin_tintuc);
        }

        // GET: Admin/TinTuc/Delete/5
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
            admin_tintuc admin_tintuc = db.admin_tintuc.Find(id);
            if (admin_tintuc == null)
            {
                return HttpNotFound();
            }
            return View(admin_tintuc);
        }

        // POST: Admin/TinTuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_tintuc admin_tintuc = db.admin_tintuc.Find(id);
            admin_tintuc.ngaycapnhat = DateTime.Now;
            admin_tintuc.daxoa = true;
            db.Entry(admin_tintuc).State = EntityState.Modified;
            db.SaveChanges();
            //db.admin_tintuc.Remove(admin_tintuc);
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

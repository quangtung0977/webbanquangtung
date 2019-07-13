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
    public class admin_accountController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/admin_account
        public ActionResult Index()
        {
            return View(db.admin_account.ToList().Where(s=>s.quyen!=1).ToList());
        }

        // GET: Admin/admin_account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_account admin_account = db.admin_account.Find(id);
            if (admin_account == null)
            {
                return HttpNotFound();
            }
            return View(admin_account);
        }

        // GET: Admin/admin_account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/admin_account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,ngaytao,landangnhapcuoi,hoten,gioitinh,ghichu,quyen,daxoa")] admin_account admin_account)
        {
            if (ModelState.IsValid)
            {
                admin_account.ngaytao = DateTime.Now;
                admin_account.landangnhapcuoi = DateTime.Now;
                db.admin_account.Add(admin_account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_account);
        }

        // GET: Admin/admin_account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_account admin_account = db.admin_account.Find(id);
            if (admin_account == null)
            {
                return HttpNotFound();
            }
            return View(admin_account);
        }

        // POST: Admin/admin_account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,ngaytao,landangnhapcuoi,hoten,gioitinh,ghichu,quyen,daxoa")] admin_account admin_account)
        {
            if (ModelState.IsValid)
            {
                admin_account.landangnhapcuoi = DateTime.Now;
                db.Entry(admin_account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_account);
        }

        // GET: Admin/admin_account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_account admin_account = db.admin_account.Find(id);
            if (admin_account == null)
            {
                return HttpNotFound();
            }
            return View(admin_account);
        }

        // POST: Admin/admin_account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            admin_account admin_account = db.admin_account.Find(id);
            db.admin_account.Remove(admin_account);
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

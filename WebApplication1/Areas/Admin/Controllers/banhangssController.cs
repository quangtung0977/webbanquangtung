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
    public class banhangssController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/banhangss
        public ActionResult Index()
        {
            return View(db.banhangs.ToList());
        }

        // GET: Admin/banhangss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banhang banhang = db.banhangs.Find(id);
            if (banhang == null)
            {
                return HttpNotFound();
            }
            return View(banhang);
        }

        // GET: Admin/banhangss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/banhangss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,madonbanhang,tongtien,tenKH,diachiKH,dienthoaiKH,ghichu,trangthaidonhang,ngaydathang,ngaycapnhat")] banhang banhang)
        {
            if (ModelState.IsValid)
            {
                db.banhangs.Add(banhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banhang);
        }

        // GET: Admin/banhangss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banhang banhang = db.banhangs.Find(id);
            if (banhang == null)
            {
                return HttpNotFound();
            }
            return View(banhang);
        }

        // POST: Admin/banhangss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,madonbanhang,tongtien,tenKH,diachiKH,dienthoaiKH,ghichu,trangthaidonhang,ngaydathang,ngaycapnhat")] banhang banhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banhang);
        }

        // GET: Admin/banhangss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banhang banhang = db.banhangs.Find(id);
            if (banhang == null)
            {
                return HttpNotFound();
            }
            return View(banhang);
        }

        // POST: Admin/banhangss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            banhang banhang = db.banhangs.Find(id);
            db.banhangs.Remove(banhang);
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

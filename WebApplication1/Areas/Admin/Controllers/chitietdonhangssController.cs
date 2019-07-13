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
    public class chitietdonhangssController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/chitietdonhangss
        public ActionResult Index()
        {
            var chitietdonhangs = db.chitietdonhangs.Include(c => c.banhang).Include(c => c.sanpham);
            return View(chitietdonhangs.ToList());
        }

        // GET: Admin/chitietdonhangss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietdonhang chitietdonhang = db.chitietdonhangs.Find(id);
            if (chitietdonhang == null)
            {
                return HttpNotFound();
            }
            return View(chitietdonhang);
        }

        // GET: Admin/chitietdonhangss/Create
        public ActionResult Create()
        {
            ViewBag.iddonbanhang = new SelectList(db.banhangs, "id", "madonbanhang");
            ViewBag.idsp = new SelectList(db.sanphams, "id", "ten");
            return View();
        }

        // POST: Admin/chitietdonhangss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,iddonbanhang,idsp,soluong,giaban,tensanpham")] chitietdonhang chitietdonhang)
        {
            if (ModelState.IsValid)
            {
                db.chitietdonhangs.Add(chitietdonhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iddonbanhang = new SelectList(db.banhangs, "id", "madonbanhang", chitietdonhang.iddonbanhang);
            ViewBag.idsp = new SelectList(db.sanphams, "id", "ten", chitietdonhang.idsp);
            return View(chitietdonhang);
        }

        // GET: Admin/chitietdonhangss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietdonhang chitietdonhang = db.chitietdonhangs.Find(id);
            if (chitietdonhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.iddonbanhang = new SelectList(db.banhangs, "id", "madonbanhang", chitietdonhang.iddonbanhang);
            ViewBag.idsp = new SelectList(db.sanphams, "id", "ten", chitietdonhang.idsp);
            return View(chitietdonhang);
        }

        // POST: Admin/chitietdonhangss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,iddonbanhang,idsp,soluong,giaban,tensanpham")] chitietdonhang chitietdonhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitietdonhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iddonbanhang = new SelectList(db.banhangs, "id", "madonbanhang", chitietdonhang.iddonbanhang);
            ViewBag.idsp = new SelectList(db.sanphams, "id", "ten", chitietdonhang.idsp);
            return View(chitietdonhang);
        }

        // GET: Admin/chitietdonhangss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chitietdonhang chitietdonhang = db.chitietdonhangs.Find(id);
            if (chitietdonhang == null)
            {
                return HttpNotFound();
            }
            return View(chitietdonhang);
        }

        // POST: Admin/chitietdonhangss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chitietdonhang chitietdonhang = db.chitietdonhangs.Find(id);
            db.chitietdonhangs.Remove(chitietdonhang);
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

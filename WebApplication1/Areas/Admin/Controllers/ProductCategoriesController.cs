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
    public class ProductCategoriesController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/ProductCategories
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            return View(db.loaisanphams.ToList());
        }

        // GET: Admin/ProductCategories/Details/5
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
            loaisanpham loaisanpham = db.loaisanphams.Find(id);
            if (loaisanpham == null)
            {
                return HttpNotFound();
            }
            return View(loaisanpham);
        }

        // GET: Admin/ProductCategories/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            return View();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,ten,ngaytao,ngaycapnhat,nguoitao,daxoa,hinhanhloaisanpham,motasoluoc,ghichu,hienthi,thutuhien")] loaisanpham loaisanpham)
        public ActionResult Create([Bind(Include = "id,ten,hinhanhloaisanpham,motasoluoc,ghichu,hienthi,thutuhien")] loaisanpham loaisanpham)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    admin_account u = Session["User"] as admin_account;
                    loaisanpham.nguoitao = u.id;
                    loaisanpham.ngaytao = DateTime.Now;
                    loaisanpham.ngaycapnhat = DateTime.Now;
                    loaisanpham.daxoa = false;
                    if (loaisanpham.hienthi == null)
                    {
                        loaisanpham.hienthi = true;
                    }

                    db.loaisanphams.Add(loaisanpham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(loaisanpham);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: Admin/ProductCategories/Edit/5
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
            loaisanpham loaisanpham = db.loaisanphams.Find(id);
            if (loaisanpham == null)
            {
                return HttpNotFound();
            }
            return View(loaisanpham);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ten,ngaytao,ngaycapnhat,nguoitao,daxoa,hinhanhloaisanpham,motasoluoc,ghichu,hienthi,thutuhien")] loaisanpham loaisanpham)
        {
            if (ModelState.IsValid)
            {
                loaisanpham.ngaycapnhat = DateTime.Now;
                db.Entry(loaisanpham).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaisanpham);
        }

        // GET: Admin/ProductCategories/Delete/5
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
            loaisanpham loaisanpham = db.loaisanphams.Find(id);
            if (loaisanpham == null)
            {
                return HttpNotFound();
            }
            return View(loaisanpham);
        }

        // POST: Admin/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            loaisanpham loaisanpham = db.loaisanphams.Find(id);
            db.loaisanphams.Remove(loaisanpham);
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

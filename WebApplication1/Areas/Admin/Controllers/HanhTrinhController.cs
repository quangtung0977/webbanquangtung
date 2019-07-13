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
    public class HanhTrinhController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/admin_hanhtrinh
        public ActionResult Index()
        {
            if(Session["User"]== null)
            {
                return RedirectToAction("Logout", "Account");
            }

            var admin_hanhtrinh = db.admin_hanhtrinh.Include(a => a.admin_account).Include(a => a.admin_loaihanhtrinh).Where(s=>s.daxoa!= true);
            return View(admin_hanhtrinh.ToList());
        }

        // GET: Admin/admin_hanhtrinh/Details/5
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
            admin_hanhtrinh admin_hanhtrinh = db.admin_hanhtrinh.Find(id);
            if (admin_hanhtrinh == null)
            {
                return HttpNotFound();
            }
            return View(admin_hanhtrinh);
        }

        // GET: Admin/admin_hanhtrinh/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            ViewBag.loaihanhtrinh = new SelectList(db.admin_loaihanhtrinh, "id", "tenloaihanhtrinh");
            return View();
        }

        // POST: Admin/admin_hanhtrinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "tenhanhtrinh,tenhanhtrinh_en,hinhdaidien,loaihanhtrinh,ngaytao,thongtinthoigian,thongtindiadiem,diadiem_en,motasoluoc,motasoluoc_en,noidung,noidung_en,hienthi,nguoitao")] admin_hanhtrinh admin_hanhtrinh)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    admin_hanhtrinh.ngaycapnhat = DateTime.Now;
                    if (Session["User"] != null)
                    {
                        admin_account u = Session["User"] as admin_account;
                        admin_hanhtrinh.nguoitao= u.id;
                    }
                    db.admin_hanhtrinh.Add(admin_hanhtrinh);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_hanhtrinh.nguoitao);
                ViewBag.loaihanhtrinh = new SelectList(db.admin_loaihanhtrinh, "id", "tenloaihanhtrinh", admin_hanhtrinh.loaihanhtrinh);
                return View(admin_hanhtrinh);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: Admin/admin_hanhtrinh/Edit/5
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
            admin_hanhtrinh admin_hanhtrinh = db.admin_hanhtrinh.Find(id);
            if (admin_hanhtrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_hanhtrinh.nguoitao);
            ViewBag.loaihanhtrinh = new SelectList(db.admin_loaihanhtrinh, "id", "tenloaihanhtrinh", admin_hanhtrinh.loaihanhtrinh);
            return View(admin_hanhtrinh);
        }

        // POST: Admin/admin_hanhtrinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,tenhanhtrinh,tenhanhtrinh_en,hinhdaidien,loaihanhtrinh,ngaytao,thongtinthoigian,thongtindiadiem,diadiem_en,motasoluoc,motasoluoc_en,noidung,noidung_en,hienthi,nguoitao")] admin_hanhtrinh admin_hanhtrinh)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_hanhtrinh.ngaycapnhat = DateTime.Now;
                db.Entry(admin_hanhtrinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_hanhtrinh.nguoitao);
            ViewBag.loaihanhtrinh = new SelectList(db.admin_loaihanhtrinh, "id", "tenloaihanhtrinh", admin_hanhtrinh.loaihanhtrinh);
            return View(admin_hanhtrinh);
        }

        // GET: Admin/admin_hanhtrinh/Delete/5
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
            admin_hanhtrinh admin_hanhtrinh = db.admin_hanhtrinh.Find(id);
            if (admin_hanhtrinh == null)
            {
                return HttpNotFound();
            }
            return View(admin_hanhtrinh);
        }

        // POST: Admin/admin_hanhtrinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_hanhtrinh admin_hanhtrinh = db.admin_hanhtrinh.Find(id);
            //db.admin_hanhtrinh.Remove(admin_hanhtrinh);
            admin_hanhtrinh.daxoa = true;
            admin_hanhtrinh.ngaycapnhat = DateTime.Now;
            db.Entry(admin_hanhtrinh).State = EntityState.Modified;
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

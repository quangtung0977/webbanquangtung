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
    public class ThanhVienController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/ThanhVien
        public ActionResult Index()
        {
            try
            {
                if (Session["User"] == null)
                {
                    return RedirectToAction("Logout", "Account");
                }
                var admin_thanhvien = db.admin_thanhvien.Include(a => a.admin_account).Include(a => a.admin_nhomthanhvien).Where(s => s.daxoa != true);
                return View(admin_thanhvien.ToList());
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: Admin/ThanhVien/Details/5
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
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            if (admin_thanhvien == null)
            {
                return HttpNotFound();
            }
            return View(admin_thanhvien);
        }

        // GET: Admin/ThanhVien/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien");
            return View();
        }

        // POST: Admin/ThanhVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,tenthanhvien,tenthanhvien_en,motasoluoc,motasoluoc_en,chitiet,chitiet_en,urlhinhanh,ngaytao,ngaycapnhat,nguoitao,daxoa,ghichu,nhomthanhvien,hienthi")] admin_thanhvien admin_thanhvien)
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
                    admin_thanhvien.nguoitao = u.id;
                }
                admin_thanhvien.ngaytao = DateTime.Now;
                admin_thanhvien.ngaycapnhat = DateTime.Now;
                db.admin_thanhvien.Add(admin_thanhvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_thanhvien.nguoitao);
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien", admin_thanhvien.nhomthanhvien);
            return View(admin_thanhvien);
        }

        // GET: Admin/ThanhVien/Edit/5
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
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            if (admin_thanhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_thanhvien.nguoitao);
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien", admin_thanhvien.nhomthanhvien);
            return View(admin_thanhvien);
        }

        // POST: Admin/ThanhVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,tenthanhvien,tenthanhvien_en,motasoluoc,motasoluoc_en,chitiet,chitiet_en,urlhinhanh,thutuhienthi,ngaytao,ngaycapnhat,nguoitao,daxoa,ghichu,nhomthanhvien,hienthi")] admin_thanhvien admin_thanhvien)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_thanhvien.ngaycapnhat = DateTime.Now;
                db.Entry(admin_thanhvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_thanhvien.nguoitao);
            ViewBag.nhomthanhvien = new SelectList(db.admin_nhomthanhvien, "id", "tennhomthanhvien", admin_thanhvien.nhomthanhvien);
            return View(admin_thanhvien);
        }

        // GET: Admin/ThanhVien/Delete/5
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
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            if (admin_thanhvien == null)
            {
                return HttpNotFound();
            }
            return View(admin_thanhvien);
        }

        // POST: Admin/ThanhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            //db.admin_thanhvien.Remove(admin_thanhvien);
            admin_thanhvien.ngaycapnhat = DateTime.Now;
            admin_thanhvien.daxoa = true;
            db.Entry(admin_thanhvien).State = EntityState.Modified;
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

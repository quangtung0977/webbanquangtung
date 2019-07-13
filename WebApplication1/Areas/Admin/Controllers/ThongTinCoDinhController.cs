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
    public class ThongTinCoDinhController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();

        // GET: Admin/ThongTinCoDinh
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            return View(db.admin_thongtincodinh.ToList());
        }

        // GET: Admin/ThongTinCoDinh/Details/5
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
            admin_thongtincodinh admin_thongtincodinh = db.admin_thongtincodinh.Find(id);
            if (admin_thongtincodinh == null)
            {
                return HttpNotFound();
            }
            return View(admin_thongtincodinh);
        }

        // GET: Admin/ThongTinCoDinh/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            return View();
        }

        // POST: Admin/ThongTinCoDinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,diachi,diachi_en,dienthoai,website,fax,mail,logo,diachigmap,baiviettrangchu,baiviettrangchu_en")] admin_thongtincodinh admin_thongtincodinh)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                db.admin_thongtincodinh.Add(admin_thongtincodinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_thongtincodinh);
        }

        // GET: Admin/ThongTinCoDinh/Edit/5
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
            admin_thongtincodinh admin_thongtincodinh = db.admin_thongtincodinh.Find(id);
            if (admin_thongtincodinh == null)
            {
                return HttpNotFound();
            }

            ViewBag.vdgmap0 = "<iframe src=\"";
            ViewBag.vdgmap1 = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.512525748049!2d106.70210481464707!3d10.772002292324546!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752f4137df72b7%3A0x1351d358bc2fe73!2zQklURVhDTywgTmfDtCDEkOG7qWMgS-G6vywgQuG6v24gTmdow6ksIFF14bqtbiAxLCBI4buTIENow60gTWluaCwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1534016393545";
            ViewBag.vdgmap2 = "\" width=\"800\" height=\"600\" frameborder=\"0\" style=\"border:0\" allowfullscreen></iframe>";
            return View(admin_thongtincodinh);
        }

        // POST: Admin/ThongTinCoDinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,diachi,diachi_en,dienthoai,website,fax,mail,logo,diachigmap,baiviettrangchu,baiviettrangchu_en")] admin_thongtincodinh admin_thongtincodinh)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.vdgmap0 = "<iframe src=\"";
            ViewBag.vdgmap1 = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.512525748049!2d106.70210481464707!3d10.772002292324546!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752f4137df72b7%3A0x1351d358bc2fe73!2zQklURVhDTywgTmfDtCDEkOG7qWMgS-G6vywgQuG6v24gTmdow6ksIFF14bqtbiAxLCBI4buTIENow60gTWluaCwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1534016393545";
            ViewBag.vdgmap2 = "\" width=\"800\" height=\"600\" frameborder=\"0\" style=\"border:0\" allowfullscreen></iframe>";
            if (ModelState.IsValid)
            {
                db.Entry(admin_thongtincodinh).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                View(admin_thongtincodinh);
            }
            return View(admin_thongtincodinh);
        }

        // GET: Admin/ThongTinCoDinh/Delete/5
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
            admin_thongtincodinh admin_thongtincodinh = db.admin_thongtincodinh.Find(id);
            if (admin_thongtincodinh == null)
            {
                return HttpNotFound();
            }
            return View(admin_thongtincodinh);
        }

        // POST: Admin/ThongTinCoDinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_thongtincodinh admin_thongtincodinh = db.admin_thongtincodinh.Find(id);
            db.admin_thongtincodinh.Remove(admin_thongtincodinh);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class LoaiHinhAnhController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();
        public class _admin_loaihinhanh
        {
            public int id { get; set; }
            public string tenloaihinhanh { get; set; }
            public Nullable<System.DateTime> ngaytao { get; set; }
            public Nullable<System.DateTime> ngaycapnhat { get; set; }
            public Nullable<int> nguoitao { get; set; }
            public Nullable<bool> hienthi { get; set; }
            public Nullable<bool> daxoa { get; set; }
            public string motahinhanh { get; set; }
            public string ghichu { get; set; }
            public string tenloaihinhanh_en { get; set; }
            public string motahinhanh_en { get; set; }
            public virtual admin_account admin_account { get; set; }
            public virtual ICollection<admin_hinhanh> admin_hinhanh { get; set; }
        }
        public class VM_LoaiHinhAnh
        {
            public admin_loaihinhanh _loaihinhanh { get; set; }
            public List<admin_hinhanh> lst_hinhanh { get; set; }
        }
        // GET: Admin/LoaiHinhAnh
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            var admin_loaihinhanh = db.admin_loaihinhanh.Include(a => a.admin_account).Where(s => s.daxoa != true && s.id != -1);
            return View(admin_loaihinhanh.ToList());
        }

        // GET: Admin/LoaiHinhAnh/Details/5
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
            admin_loaihinhanh admin_loaihinhanh = db.admin_loaihinhanh.Find(id);
            if (admin_loaihinhanh == null)
            {
                return HttpNotFound();
            }

            var imagesModel = new admin_hinhanh();
            //var imageFiles = Directory.GetFiles(Server.MapPath("~/UserUpload/images/HinhAnh/"));
            var imageFiles = db.admin_hinhanh.Where(s => s.loaihinhanh == admin_loaihinhanh.id).ToList();
            List<string> lstImg = new List<string>();
            foreach (var item in imageFiles)
            {
                lstImg.Add(item.urlhinhanh);
            }
            ViewBag._lstImg = lstImg;
            Session["idLoaiHinhAnh"] = admin_loaihinhanh.id;

            return View();
        }

        [HttpGet]
        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImageMethod()
        {
            if (Request.Files.Count != 0)
            {
                string uri=Request.RawUrl;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    file.SaveAs(Server.MapPath("~/UserUpload/images/HinhAnh/" + fileName));
                    //ImageGallery imageGallery = new ImageGallery();
                    //imageGallery.ID = Guid.NewGuid();
                    //imageGallery.Name = fileName;
                    //imageGallery.ImagePath = "~/UserUpload/images/" + fileName;
                    //db.ImageGallery.Add(imageGallery);
                    //db.SaveChanges();

                    admin_hinhanh admin_hinhanh = new admin_hinhanh();
                    if (Session["User"] != null)
                    {
                        admin_account u = Session["User"] as admin_account;
                        admin_hinhanh.nguoitao = u.id;
                    }
                    admin_hinhanh.ngaytao = DateTime.Now;
                    admin_hinhanh.ngaycapnhat = DateTime.Now;
                    admin_hinhanh.tenhinhanh = fileName;
                    admin_hinhanh.urlhinhanh = "/UserUpload/images/HinhAnh/" + fileName;
                    admin_hinhanh.loaihinhanh = Convert.ToInt32(Session["idLoaiHinhAnh"]);
                    db.admin_hinhanh.Add(admin_hinhanh);
                    db.SaveChanges();

                }
                return Content("Success");
            }
            return Content("failed");
        }

        // GET: Admin/LoaiHinhAnh/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username");
            return View();
        }

        // POST: Admin/LoaiHinhAnh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,tenloaihinhanh,tenloaihinhanh_en,ngaytao,ngaycapnhat,nguoitao,hienthi,daxoa,motahinhanh,motahinhanh_en,ghichu")] admin_loaihinhanh admin_loaihinhanh)
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
                    admin_loaihinhanh.nguoitao = u.id;
                }
                admin_loaihinhanh.ngaytao = DateTime.Now;
                admin_loaihinhanh.ngaycapnhat = DateTime.Now;
                db.admin_loaihinhanh.Add(admin_loaihinhanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_loaihinhanh.nguoitao);
            return View(admin_loaihinhanh);
        }

        // GET: Admin/LoaiHinhAnh/Edit/5
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
            Session["idLoaiHinhAnh"]= id;
            admin_loaihinhanh admin_loaihinhanh = db.admin_loaihinhanh.Find(id);
            if (admin_loaihinhanh == null)
            {
                return HttpNotFound();
            }
            VM_LoaiHinhAnh vmEdit = new VM_LoaiHinhAnh();
            vmEdit._loaihinhanh = admin_loaihinhanh;
            List<admin_hinhanh> arrHinhanh = db.admin_hinhanh.Where(s => s.loaihinhanh == admin_loaihinhanh.id).ToList();
            ViewBag.arrHinhAnh = arrHinhanh;

            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_loaihinhanh.nguoitao);
            return View(admin_loaihinhanh);
        }

        // POST: Admin/LoaiHinhAnh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,tenloaihinhanh,tenloaihinhanh_en,ngaytao,ngaycapnhat,nguoitao,hienthi,daxoa,motahinhanh,motahinhanh_en,ghichu")] admin_loaihinhanh admin_loaihinhanh)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            if (ModelState.IsValid)
            {
                admin_loaihinhanh.ngaycapnhat = DateTime.Now;
                db.Entry(admin_loaihinhanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoitao = new SelectList(db.admin_account, "id", "username", admin_loaihinhanh.nguoitao);
            return View(admin_loaihinhanh);
        }

        // GET: Admin/LoaiHinhAnh/Delete/5
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
            admin_loaihinhanh admin_loaihinhanh = db.admin_loaihinhanh.Find(id);
            if (admin_loaihinhanh == null)
            {
                return HttpNotFound();
            }
            return View(admin_loaihinhanh);
        }

        // POST: Admin/LoaiHinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            admin_loaihinhanh admin_loaihinhanh = db.admin_loaihinhanh.Find(id);
            //db.admin_loaihinhanh.Remove(admin_loaihinhanh);
            admin_loaihinhanh.ngaycapnhat = DateTime.Now;
            admin_loaihinhanh.daxoa = true;
            db.Entry(admin_loaihinhanh).State = EntityState.Modified;
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


        // POST: Admin/LoaiHinhAnh/Delete/5
        [HttpPost]
        public ActionResult EditImg(string urlImgList)
        {
            try
            {
                var img_ = urlImgList.Split(';');
                var loaihinhanhId = Convert.ToInt32(Session["idLoaiHinhAnh"]);
                var listdel = db.admin_hinhanh.Where(s => s.loaihinhanh == loaihinhanhId);
                foreach (var item1 in listdel)
                {
                    using (var db1 = new WebApplication1Entities())
                    {
                        try
                        {
                            var objhinhanh = db1.admin_hinhanh.Find(item1.id);
                            db1.admin_hinhanh.Attach(objhinhanh);
                            db1.admin_hinhanh.Remove(objhinhanh);
                            db1.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }
                }
                using (var db2 = new WebApplication1Entities())
                {
                    foreach (var item in img_)
                    {
                        if (item.Length > 0)
                        {
                            admin_hinhanh admin_hinhanh = new admin_hinhanh();
                            admin_hinhanh.tenhinhanh = "img" + DateTime.Now.Year.ToString() + "" + DateTime.Now.Month + "" + DateTime.Now.Day;
                            admin_hinhanh.urlhinhanh = item;
                            admin_hinhanh.ngaytao = DateTime.Now;
                            admin_hinhanh.ngaycapnhat = DateTime.Now;
                            admin_account u = Session["User"] as admin_account;
                            admin_hinhanh.nguoitao = u.id;
                            admin_hinhanh.loaihinhanh = loaihinhanhId;
                            db2.admin_hinhanh.Add(admin_hinhanh);
                            db2.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Edit", "LoaiHinhAnh", new { id = loaihinhanhId }); // Redirect to your NextView
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

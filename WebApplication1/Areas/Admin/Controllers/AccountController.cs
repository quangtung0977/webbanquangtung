using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(admin_account account)
        {
            admin_account u = db.admin_account.Where(m => m.username == account.username && m.password == account.password).FirstOrDefault();
            if (u != null)
            {
                Session["User"] = u;

                DateTime _now = System.DateTime.Now;
                u.landangnhapcuoi = _now;
                db.admin_account.Attach(u);
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Products");
            }
            else
            {
                Session["User"] = null;
                ViewBag.Message = "";
                ViewBag.Message = "Sai tài khoản hoặc mật khẩu";
                return View();
            }
        }
        public ActionResult Logout()
        {
            admin_account u = new admin_account();
            u = Session["User"] as admin_account;
            try
            {
                if (u != null)
                {
                    u.landangnhapcuoi = DateTime.Now;
                    db.admin_account.Attach(u);
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            Session["User"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}
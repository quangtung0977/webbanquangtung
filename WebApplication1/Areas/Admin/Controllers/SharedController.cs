using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class SharedController : Controller
    {
        private WebApplication1Entities db = new WebApplication1Entities();
        // GET: Admin/Shared
        public ActionResult _LoginName()
        {
            try
            {
                if (Session["User"] != null)
                {
                    admin_account u = Session["User"] as admin_account;
                    admin_account currentuser = db.admin_account.Find(u.id);
                    currentuser.landangnhapcuoi = DateTime.Now;
                    db.Entry(currentuser).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.LoginName = currentuser.hoten;
                    return PartialView("_LoginName");
                }
                else
                {
                    Session.Clear();
                    return PartialView("_LoginName");
                }
            }
            catch (Exception ex)
            {
            }
            Session.Clear();
            return PartialView("_LoginName");
        }
    }
}
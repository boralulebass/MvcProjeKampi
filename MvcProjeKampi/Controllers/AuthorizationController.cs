using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        AdminManager am = new AdminManager(new EFAdminDal());
        public ActionResult Index()
        {
            var adminvalues =am.GetList();
            return View(adminvalues);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            am.AdminAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAdmin(Admin admin) 
        {
            am.AdminDelete(admin);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateAdmin(int id)
        {
            var catvalue = am.GetByID(id);
            return View(catvalue);
        }
        [HttpPost]
        public ActionResult UpdateAdmin(Admin admin)
        {
            am.AdminUpdate(admin);
            return RedirectToAction("Index");
        }

    }
}
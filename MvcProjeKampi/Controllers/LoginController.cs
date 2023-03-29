using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        WriterLoginManager wlm = new WriterLoginManager(new EFWriterDal());
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            Context c = new Context();
            var adminuserinfo = c.Admins.FirstOrDefault(x=>x.AdminUsername == admin.AdminUsername && x.AdminPassword == admin.AdminPassword);
            if (adminuserinfo != null) 
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUsername, false);
                Session["AdminUsername"] = adminuserinfo.AdminUsername;
                return RedirectToAction("Index","AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer writer)
        {
            //Context c = new Context();
            //var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == x.WriterPassword);
            var writeruserinfo = wlm.GetWriter(writer.WriterMail, writer.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                return RedirectToAction("WriterLogin");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings","Default");
        }
    }
}
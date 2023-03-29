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
    public class AboutController : Controller
    {
        // GET: About
        AboutManager am = new AboutManager(new EFAboutDal());
        [Authorize(Roles="A")]
        public ActionResult Index()
        {
            var aboutvalues = am.GetList();
            return View(aboutvalues);
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult AddAbout() 
        {
            return View();
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            am.AboutAdd(about);
            return RedirectToAction("Index");
        }
        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
    }
}
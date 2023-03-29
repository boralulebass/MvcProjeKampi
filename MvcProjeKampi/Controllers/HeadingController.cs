using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        // GET: Heading
        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        WriterManager wm = new WriterManager(new EFWriterDal());
        public ActionResult Index()
        {
            var headvalues = hm.GetList();
            return View(headvalues);
        }
        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> valuecat = (from x in cm.GetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryID.ToString()
                                             }).ToList();
            List<SelectListItem> valuewriter = (from x in wm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " " + x.WriterSurname,
                                                    Value = x.WriterID.ToString()
                                                }).ToList();
            ViewBag.vlc = valuecat;
            ViewBag.vlw = valuewriter;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.HeadingAdd(heading);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecat = (from x in cm.GetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryID.ToString()
                                             }).ToList();
            ViewBag.vlc = valuecat;
            var headvalue = hm.GetByID(id);
            return View(headvalue);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            hm.HeadingUpdate(heading);   
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult DeleteHeading(int id) 
        {
            var valuehead = hm.GetByID(id);
            if (valuehead.HeadingStatus == true)
            {
                valuehead.HeadingStatus = false;
                hm.HeadingRemove(valuehead);
            }
            else
            {
                valuehead.HeadingStatus = true;
                hm.HeadingRemove(valuehead);
            }
            return RedirectToAction("Index");
        }
    }
}
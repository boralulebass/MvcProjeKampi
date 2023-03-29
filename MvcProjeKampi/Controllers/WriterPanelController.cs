using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel
        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        WriterManager wm = new WriterManager(new EFWriterDal());
        Context c = new Context();
        [HttpGet]
        public ActionResult WriterProfile(int id = 0)
        {
            string p = (string)Session["WriterMail"];
            id  = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var writervalue=wm.GetByID(id);
            return View(writervalue);
        }
        [HttpPost]
        public ActionResult WriterProfile(Writer writer)
        {
            WriterValidator validator = new WriterValidator();
            ValidationResult validationResult = validator.Validate(writer);
            if (validationResult.IsValid)
            {
                wm.WriterUpdate(writer);
                return RedirectToAction("WriterProfile");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult MyHeading(string p)
        {
            Context c = new Context();
            p = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x=>x.WriterMail==p) .Select(y=>y.WriterID).FirstOrDefault();
            var values = hm.GetListByWriter(writeridinfo);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valuecat = (from x in cm.GetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryID.ToString()
                                             }).ToList();
            ViewBag.vlc = valuecat;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            string writermailinfo = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == writermailinfo).Select(y => y.WriterID).FirstOrDefault();
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterID = writeridinfo;
            heading.HeadingStatus = true;
            hm.HeadingAdd(heading);
            return RedirectToAction("MyHeading");
        }
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
        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            hm.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }
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
            return RedirectToAction("MyHeading");
        }
        public ActionResult AllHeading(int p = 1)
        {
            var headings = hm.GetList().ToPagedList(p,5);
            return View(headings);
        }
    }
}
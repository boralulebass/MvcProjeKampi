using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        ImageManager im = new ImageManager(new EFImageDal());
        public ActionResult Index()
        {
            var values = im.GetList();
            return View(values);
        }
    }
}
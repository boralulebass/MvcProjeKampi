using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        public ActionResult Index()
        {
            var catvalues = cm.GetList();
            return View(catvalues);
        }
        [HttpGet]
        public ActionResult AddCategory() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CategoryValidator validationRules = new CategoryValidator();
            ValidationResult result = validationRules.Validate(category);
            if(result.IsValid) 
            {
                cm.CategoryAdd(category);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors) 
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
        public ActionResult DeleteCategory (int id) 
        {
            var catvalue = cm.GetByID(id);
            cm.CategoryRemove(catvalue);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateCategory(int id) 
        {
            var catvalue = cm.GetByID(id);
            return View(catvalue);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            cm.CategoryUpdate(category);
            return RedirectToAction("Index");
        }
    }
}
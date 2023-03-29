using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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
    public class WriterPanelMessageController : Controller
    {
        
        MessageManager mm = new MessageManager(new EFMessageDal());
        MessageValidator validator = new MessageValidator();

        public ActionResult WriterProfile()
        {
            return View();
        }
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];  
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public PartialViewResult PartialMessageListMenu()
        {
            string p = (string)Session["WriterMail"];
            ViewBag.valueinb = mm.GetListInbox(p).Count();
            ViewBag.valuesnb = mm.GetListSendbox(p).Count();
            return PartialView();
        }
        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var contactval = mm.GetByID(id);
            return View(contactval);
        }
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var contactval = mm.GetByID(id);
            return View(contactval);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            string sender = (string)Session["WriterMail"];
            ValidationResult validationResult = validator.Validate(message);
            if (validationResult.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
                message.SenderMail = sender;
                mm.MessageAdd(message);
                return RedirectToAction("Sendbox");
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
    }
}
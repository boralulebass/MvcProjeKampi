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
    [Authorize(Roles = "A")]
    public class MessageController : Controller
    {
        

        // GET: Message
        MessageManager mm = new MessageManager(new EFMessageDal());
        MessageValidator validator = new MessageValidator();
        
        public ActionResult Inbox(string p)
        {
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult Sendbox(string p) 
        {
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
            ValidationResult validationResult = validator.Validate(message);
            if (validationResult.IsValid)
            {
                message.MessageDate=DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
                message.SenderMail = "admin@gmail.com";
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
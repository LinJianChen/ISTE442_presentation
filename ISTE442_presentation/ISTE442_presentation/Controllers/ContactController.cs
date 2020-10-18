using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISTE_422_presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ISTE_422_presentation.Controllers
{
    //set route
    [Route("contact")]
    public class ContactController : Controller
    {
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            ContactStoreContext context = HttpContext.RequestServices.GetService(typeof(ISTE_422_presentation.Models.ContactStoreContext)) as ContactStoreContext;

            ViewBag.contacts = context.GetContacts();

            return View();
        }
        [HttpGet]
        [Route("delete/{contactId}")]
        public IActionResult Delete(String contactId)
        {
            ContactStoreContext context = HttpContext.RequestServices.GetService(typeof(ISTE_422_presentation.Models.ContactStoreContext)) as ContactStoreContext;
            context.deleteContact(int.Parse(contactId));

            return RedirectToAction("Index");

        }
        [Route("Add")]
        public IActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Contact contact)
        {
            ContactStoreContext context = HttpContext.RequestServices.GetService(typeof(ISTE_422_presentation.Models.ContactStoreContext)) as ContactStoreContext;
            context.addContact(contact);

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("edit/{contactId}")]
        public IActionResult Edit(string contactId)
        {

            ContactStoreContext context = HttpContext.RequestServices.GetService(typeof(ISTE_422_presentation.Models.ContactStoreContext)) as ContactStoreContext;

            var result = context.getContact(int.Parse(contactId));
            return View("Edit", result);
        }
        [HttpPost]
        [Route("edit/{contactId}")]
        public IActionResult Edit(string contactId, Contact contact)
        {
            ContactStoreContext context = HttpContext.RequestServices.GetService(typeof(ISTE_422_presentation.Models.ContactStoreContext)) as ContactStoreContext;

            context.updateContact(int.Parse(contactId), contact);


            return RedirectToAction("Index");
        }
    }
}

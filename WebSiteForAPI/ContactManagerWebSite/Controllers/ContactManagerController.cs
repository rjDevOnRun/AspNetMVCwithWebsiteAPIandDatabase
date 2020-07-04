using ContactManagerWebSite.Models;
using ContactManagerWebSite.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using WebService.Models;

namespace ContactManagerWebSite.Controllers
{
    public class ContactManagerController : Controller
    {
        public async System.Threading.Tasks.Task<ActionResult> SaveContact()
        {

            var contact = new Contact
            {
                Name = "Name from WebSite UI 1",
                PhoneNumber = "00001111",
                Note = "Note from WebSite UI 1"
            };

            if (contact == null) ViewBag.Message = "Model was not Bound";
            else
            {

                var success = await ContactManagerProcessor.ProcessContact(contact);

                if (success == true)
                    ViewBag.Message = "Contact Saved Successfully!";
                else
                    ViewBag.Message = "Contact Save Failed!";
            }

            return View();
        }


        /* ==================================================================================================
         * NOTE:    for post actions, always create two view methods
         *          one for the actual View to get object information with form
         *          next for the HTTP Post Action for posting form values to API/Db
         */

        public ActionResult CreateContact()
        {
            return View(new ContactModel());
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateContact(ContactModel contact)
        {
            var saveResult = await CreateAndSaveContact(contact);

            if (saveResult == true)
                ViewBag.Message = $"{contact.Name}: Contact Saved Successfully!";
            else
                ViewBag.Message = $"{contact.Name}: Contact Save Failed!";

            return View("SaveContact");
        }
        // ==================================================================================================

        private async System.Threading.Tasks.Task<bool> CreateAndSaveContact(ContactModel contactModel)
        {
            var contact = new Contact
            {
                Name = contactModel.Name,
                PhoneNumber = contactModel.PhoneNumber,
                Note = contactModel.Note
            };

            return await ContactManagerProcessor.ProcessContact(contact);
        }


        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}
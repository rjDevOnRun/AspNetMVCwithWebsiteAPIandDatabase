using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;
using WebService.Processors;

namespace WebService.Controllers
{
    public class ContactsController : ApiController
    {
        // POST api/values
        [HttpPost]
        [Route("SaveContact")]
        public bool SaveContact(Contact contact)
        {
            if (contact == null) return false;

            return ContactProcessor.ProcessContact(contact);

        }


        // GET: List of Contacts
        [Route("GetContactsInDB")]
        public List<Contact> GetContactInDatabase()
        {
            return ContactProcessor.GetContacts();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }


        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}

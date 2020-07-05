using ContactManagerWebSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebService.Models;

namespace ContactManagerWebSite.Processor
{
    public class ContactManagerProcessor
    {

        public static async Task<bool> ProcessContact(Contact contact)
        {

            return await ContactManagerRepository.SaveContact(contact);

        }

        public static List<Contact> GetAllContacts()
        {
            return ContactManagerRepository.GetContacts();
        }

    }
}
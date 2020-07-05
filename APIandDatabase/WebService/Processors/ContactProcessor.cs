using ContactManagerWebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using WebService.Models;
using WebService.Repositories;

namespace WebService.Processors
{
    public class ContactProcessor
    {
        public static List<Contact> GetContacts()
        {
            DataTable dt = ContactRepository.GetContactsFromDatabase();
            List<Contact> contacts = new List<Contact>();
            contacts = AssemblyHelpers.ConvertDataTable<Contact>(dt);
            return contacts;

        }

        public static bool ProcessContact(Contact contact)
        {

            return ContactRepository.AddContactToDB(contact);

        }


    }
}
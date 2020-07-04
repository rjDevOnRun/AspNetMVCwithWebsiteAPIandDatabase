using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactManagerWebSite.Models
{
    public class ContactModel
    {
        
        [DisplayName("Contact Name")]
        public string Name { get; set; }
        
        [DisplayName("Contact Phone Number")]
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
    }
}
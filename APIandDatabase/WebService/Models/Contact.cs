using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace WebService.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
    }
}
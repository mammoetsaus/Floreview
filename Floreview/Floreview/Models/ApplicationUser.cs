using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String AccessCode { get; set; }
    }
}
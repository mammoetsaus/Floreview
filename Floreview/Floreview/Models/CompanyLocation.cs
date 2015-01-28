using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class CompanyLocation
    {
        public int ID { get; set; }

        public virtual Company Company { get; set; }

        public virtual Location Location { get; set; }
    }
}
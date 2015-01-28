using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class Blog
    {
        public int ID { get; set; }

        public String Avatar { get; set; }

        public String Title { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual BlogCategory Category { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
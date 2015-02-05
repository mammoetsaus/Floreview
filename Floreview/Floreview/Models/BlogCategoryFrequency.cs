using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class BlogCategoryFrequency
    {
        public int CategoryID { get; set; }

        public String Category { get; set; }

        public int Frequency { get; set; }
    }
}
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels
{
    public class BlogVM
    {
        public int BlockNumber { get; set; }

        public String Author { get; set; }

        public int CategoryID { get; set; }

        public String Query { get; set; }

        public String Date { get; set; }

        public List<BlogCategory> Categories { get; set; }

        public int[] CategoryFrequencies { get; set; }

        public Dictionary<DateTime, int> Dates { get; set; }
    }
}
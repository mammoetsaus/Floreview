using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels.CMS
{
    public class BlogCategoryVM
    {
        public BlogCategory BlogCategory { get; set; }

        public List<BlogCategory> BlogCategories { get; set; }
    }
}
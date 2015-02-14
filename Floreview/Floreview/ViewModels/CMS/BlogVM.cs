using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.ViewModels.CMS
{
    public class BlogVM
    {
        public Blog Blog { get; set; }

        public List<BlogElement> BlogElements { get; set; }

        public SelectList BlogCategories { get; set; }

        public int SelectedBlogCategoryID { get; set; }

        public HttpPostedFileBase BlogAvatar { get; set; }

        public HttpPostedFileBase[] BlogPictures { get; set; }
    }
}
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels
{
    public class DetailBlogVM
    {
        public Blog Blog { get; set; }

        public List<Blog> RelatedBlogs { get; set; }
    }
}
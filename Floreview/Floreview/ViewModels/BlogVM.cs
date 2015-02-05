using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels
{
    public class BlogVM
    {
        public String Query { get; set; }

        public List<Blog> Blogs { get; set; }

        public List<BlogCategoryFrequency> BlogCategoryFrequencies { get; set; }

        public List<BlogPublishdateFrequency> BlogPublishdateFrequencies { get; set; }

        public List<BlogAuthorFrequency> BlogAuthorFrequencies { get; set; }
    }
}
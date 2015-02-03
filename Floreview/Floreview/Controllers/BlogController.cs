using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers
{
    public class BlogController : Controller
    {
        private IAccessService _accessService = null;

        private const int BLOCKSIZE = 1;

        public BlogController(IAccessService service)
        {
            _accessService = service;
        }

        public BlogController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int? blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.HasValue && blog > 0)
                {
                    BlogVM model = new BlogVM();
                    model.Blog = _accessService.GetBlogByID(blog.Value);

                    if (model.Blog != null && DateTime.Compare(model.Blog.PublishDate, DateTime.Now) <= 0)
                    {
                        return View(model);
                    }
                }
            }

            return RedirectToAction("Index", "Blog");
        }
    }
}
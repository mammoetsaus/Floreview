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

        public BlogController(IAccessService service)
        {
            _accessService = service;
        }

        public BlogController()
        {

        }

        public ActionResult Index(int? page, String query, int? category, String archive, String author)
        {
            if (ModelState.IsValid)
            {
                BlogVM model = new BlogVM();

                if (!String.IsNullOrEmpty(query)) model.Query = query;

                model.BlogCategoryFrequencies = _accessService.GetAllBlogFrequencies();
                model.BlogPublishdateFrequencies = _accessService.GetAllBlogPublishdateFrequencies();
                model.Blogs = _accessService.GetBlogsWithFilters(page, query, category, archive, author);
                model.BlogAuthorFrequencies = _accessService.GetAllBlogAuthorFrequencies();

                return View(model);
            }

            return RedirectToAction("Index", "Blog");
        }

        public ActionResult Detail(int? blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.HasValue && blog > 0)
                {
                    DetailBlogVM model = new DetailBlogVM();
                    model.Blog = _accessService.GetBlogByID(blog.Value);

                    if (model.Blog != null && (DateTime.Compare(model.Blog.PublishDate, DateTime.UtcNow) <= 0 || User.Identity.IsAuthenticated))
                    {
                        model.RelatedBlogs = _accessService.GetRelatedBlogs(3, model.Blog.ID, model.Blog.Category.ID);

                        return View(model);
                    }
                }
            }

            return RedirectToAction("Index", "Blog");
        }
    }
}
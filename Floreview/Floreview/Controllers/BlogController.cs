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

        public ActionResult BlogList(List<Blog> Model)
        {
            // only shown once at startup
            // called after for markup
            return PartialView(Model);
        }

        public ActionResult SideBlog(BlogVM model)
        {
            model.Dates = _accessService.GetDatesForSideBlog();
            model.Categories = _accessService.GetBlogCategoriesForSideBlog();

            if (model.Categories != null && model.Categories.Count > 0)
            {
                List<Blog> lstBlogs = _accessService.GetAllBlogs();

                model.CategoryFrequencies = (from c in lstBlogs orderby c.Category.Name group c by c.Category into g select g.Count()).ToArray();
            }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult InfiniteScroll(BlogVM model)
        {
            List<Blog> lstNewBlogs = _accessService.GetNextRangeOfBlogs(model, BLOCKSIZE);

            BlogJSON modelJSON = new BlogJSON();
            modelJSON.NoMoreData = lstNewBlogs.Count < BLOCKSIZE;
            modelJSON.HTMLString = RenderPartialViewToString("BlogList", lstNewBlogs);

            return Json(modelJSON);
        }

        public ActionResult Index()
        {
            return View();
        }

        protected String RenderPartialViewToString(String viewName, object model)
        {
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
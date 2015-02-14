using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Floreview.Controllers.API
{
    public class RESTController : ApiController
    {
        private IAccessService _accessService = null;

        public RESTController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        public RESTController()
        {

        }

        [Route("api/cities")]
        public HttpResponseMessage GetCitiesByName(String city)
        {
            HttpResponseMessage message = null;

            if (ModelState.IsValid)
            {
                if (city.Length >= 3) 
                {
                    List<Location> lstCities = _accessService.GetLocationsByQuery(city);

                    message = new HttpResponseMessage(HttpStatusCode.OK);
                    message.Content = new ObjectContent<List<Location>>(lstCities, Configuration.Formatters[0], "application/json");
                    return message;
                }
            }

            message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new ObjectContent<ApiErrorMessage>(new ApiErrorMessage() { Success = false, Message = "Couldn't load locations." }, Configuration.Formatters[0], "application/json");
            return message;
        }

        [Authorize]
        [Route("api/companies")]
        public HttpResponseMessage GetCompanies(String filter, int sort)
        {
            HttpResponseMessage message = null;

            if (ModelState.IsValid)
            {
                List<Company> companies = _accessService.GetCompaniesByFilterAndSortMethod(filter, sort);

                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new ObjectContent<List<Company>>(companies, Configuration.Formatters[0], "application/json");
                return message;
            }

            message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new ObjectContent<ApiErrorMessage>(new ApiErrorMessage() { Success = false, Message = "Couldn't load companies." }, Configuration.Formatters[0], "application/json");
            return message;
        }

        [Authorize]
        [Route("api/blogs")]
        public HttpResponseMessage GetBlogs(String filter, int sort)
        {
            HttpResponseMessage message = null;

            if (ModelState.IsValid)
            {
                List<Blog> blogs = _accessService.GetBlogsByFilterAndSortMethod(filter, sort);

                foreach (Blog blog in blogs)
                {
                    blog.Author = new ApplicationUser()
                    {
                        FirstName = blog.Author.FirstName,
                        LastName = blog.Author.LastName,
                        AccessCode = blog.Author.AccessCode
                    };
                }

                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new ObjectContent<List<Blog>>(blogs, Configuration.Formatters[0], "application/json");
                return message;
            }

            message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new ObjectContent<ApiErrorMessage>(new ApiErrorMessage() { Success = false, Message = "Couldn't load blogs."}, Configuration.Formatters[0], "application/json");
            return message;
        }
    }
}

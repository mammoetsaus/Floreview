﻿using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            try
            {
                if (city.Length >= 3) 
                {
                    List<Location> lstCities = _accessService.GetLocationsByQuery(city);

                    message = new HttpResponseMessage(HttpStatusCode.OK);
                    message.Content = new ObjectContent<List<Location>>(lstCities, Configuration.Formatters[0], "application/json");
                }
                else
                {
                    message = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    return message;
                }
            }
            catch (Exception)
            {
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return message;
        }

        [Authorize]
        [Route("api/companies")]
        public HttpResponseMessage GetCompanies(String filter, int sort)
        {
            HttpResponseMessage message = null;

            try
            {
                List<Company> lstCompanies = _accessService.GetCompaniesByFilterAndSortMethod(filter, sort);

                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new ObjectContent<List<Company>>(lstCompanies, Configuration.Formatters[0], "application/json");
            }
            catch (Exception)
            {
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return message;
        }
    }
}

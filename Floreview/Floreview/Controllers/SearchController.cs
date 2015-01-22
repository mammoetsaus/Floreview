using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.Utils;
using Floreview.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers
{
    public class SearchController : Controller
    {
        #region Fields & Props
        private IAccessService _accessService = null;
        #endregion

        #region Constructor
        public SearchController(IAccessService service)
        {
            _accessService = service;
        }

        public SearchController()
        {

        }
        #endregion

        #region Actions
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(SearchVM model)
        {
            try
            {
                ResultsVM result = new ResultsVM();

                if (ModelState.IsValid)
                {

                    #region User Search
                    // check if model has values
                    if (String.IsNullOrEmpty(model.SearchName) && String.IsNullOrEmpty(model.SearchCity))
                    {
                        throw new ArgumentException();
                    }
                    else if (!String.IsNullOrEmpty(model.SearchName) && String.IsNullOrEmpty(model.SearchCity))
                    {
                        // only search on florist or company name
                        result.Companies = _accessService.GetCompaniesSearchName(model.SearchName);

                        // set variables
                        ViewBag.SearchName = model.SearchName;
                        ViewBag.SearchCity = "-";
                    }
                    else if (String.IsNullOrEmpty(model.SearchName) && !String.IsNullOrEmpty(model.SearchCity))  
                    {
                        // only search on city name
                        result.Companies = _accessService.GetCompaniesSearchCity(model.SearchCity);

                        // set variables
                        ViewBag.SearchName = "-";
                        ViewBag.SearchCity = model.SearchCity;
                    }
                    else if (!String.IsNullOrEmpty(model.SearchName) && !String.IsNullOrEmpty(model.SearchCity))
                    {
                        // search both variables
                        result.Companies = _accessService.GetCompaniesSearchBoth(model.SearchName, model.SearchCity);

                        // set variables
                        ViewBag.SearchName = model.SearchName;
                        ViewBag.SearchCity = model.SearchCity;
                    }


                    // collect variables
                    ViewBag.Results = result.Companies.Count;
                    #endregion

                    #region Nearby Search
                    if (!String.IsNullOrEmpty(model.SearchCity) && !String.IsNullOrEmpty(model.LatitudeRAW) && !String.IsNullOrEmpty(model.LongitudeRAW)) {
                        // user explicitly searched for a city
                        List<Company> companies = _accessService.GetAllCompanies();

                        // create position
                        DbGeography cityCoordinates = DbGeography.FromText("POINT(" + model.LongitudeRAW + " " + model.LatitudeRAW + ")");

                        HaversineEngine engine = new HaversineEngine();
                        result.NearbyCompanies = new List<Company>();
                        foreach (Company company in companies)
                        {
                            if (engine.IsStoreWhitinRange(company.Coordinates, cityCoordinates) && !company.Location.City.Equals(model.SearchCity))
                            {
                                result.NearbyCompanies.Add(company);
                            }
                        }
                    }
                    #endregion
                }

                return View(result);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
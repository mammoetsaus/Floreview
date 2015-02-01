using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.Resources;
using Floreview.Utils;
using Floreview.ViewModels.CMS;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers.CMS
{
    [Authorize]
    public class ManageController : Controller
    {
        private IAccessService _accessService = null;

        public ManageController(IAccessService service)
        {
            _accessService = service;
        }

        public ManageController()
        {

        }

        public ActionResult Store()
        {
            return View();
        }

        public ActionResult AddStore()
        {
            StoreVM model = new StoreVM();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStore(StoreVM model)
        {
            if (ModelState.IsValid)
            {
                model.Company.Coordinates = GeocodingEngine.GetLatLongFromAddress(String.Format("{0}, {1}", model.Company.Location.City, model.Company.Address));
                model.Company.Location = _accessService.GetLocationByCityName(model.Company.Location.City);
                model.Company.Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
                model.Company.Florist.ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
                Company insertedCompany = _accessService.InsertCompany(model.Company);

                #region Company Avatar
                if (model.CompanyAvatar != null)
                {
                    insertedCompany.Avatar = BlobStorageEngine.UploadCompanyAvatar(model.CompanyAvatar, insertedCompany);
                }
                else
                {
                    insertedCompany.Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
                }
                #endregion

                #region Florist Avatar
                if (model.FloristAvatar != null)
                {
                    insertedCompany.Florist.ImagePath = BlobStorageEngine.UploadFloristAvatar(model.FloristAvatar, insertedCompany);
                }
                else
                {
                    insertedCompany.Florist.ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
                }
                #endregion

                #region Imagelist
                if (model.CompanyImages[0] != null)
                {
                    insertedCompany.ImageList = BlobStorageEngine.UploadCompanyImages(model.CompanyImages, insertedCompany);
                }
                #endregion

                _accessService.UpdateCompany(insertedCompany);

                return RedirectToAction("Store", "Manage");
            }

            return View(model);
        }

        public ActionResult EditStore(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue && id > 0)
                {
                    Company company = _accessService.GetCompanyByID(id.Value);

                    if (company != null)
                    {
                        StoreVM model = new StoreVM();
                        model.Company = company;

                        return View(model);
                    }
                }
            }

            return RedirectToAction("Store", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStore(StoreVM model)
        {
            if (ModelState.IsValid)
            {
                Company company = _accessService.GetCompanyByID(model.Company.ID);

                company.Coordinates = GeocodingEngine.GetLatLongFromAddress(String.Format("{0}, {1}", model.Company.Location.City, model.Company.Address));
                company.Location = _accessService.GetLocationByCityName(model.Company.Location.City);
                company.Name = model.Company.Name;
                company.Facebook = model.Company.Facebook;
                company.Website = model.Company.Website;
                company.Address = model.Company.Address;
                company.Email = model.Company.Email;

                company.Florist.FirstName = model.Company.Florist.FirstName;
                company.Florist.LastName = model.Company.Florist.LastName;
                company.Florist.Phone = model.Company.Florist.Phone;
                company.Florist.Cellphone = model.Company.Florist.Cellphone;

                company.DescriptionShortNL = model.Company.DescriptionShortNL;
                company.DescriptionLongNL = model.Company.DescriptionLongNL;
                company.DescriptionShortEN = model.Company.DescriptionShortEN;
                company.DescriptionLongEN = model.Company.DescriptionLongEN;
                company.DescriptionShortFR = model.Company.DescriptionShortFR;
                company.DescriptionLongFR = model.Company.DescriptionLongFR;
                company.DescriptionShortDE = model.Company.DescriptionShortDE;
                company.DescriptionLongDE = model.Company.DescriptionLongDE;


                #region Company Avatar
                if (model.CompanyAvatar != null)
                {
                    company.Avatar = BlobStorageEngine.UploadCompanyAvatar(model.CompanyAvatar, company);
                }
                #endregion

                #region Florist Avatar
                if (model.FloristAvatar != null)
                {
                    company.Florist.ImagePath = BlobStorageEngine.UploadFloristAvatar(model.FloristAvatar, company);
                }
                #endregion

                #region Imagelist
                if (model.CompanyImages[0] != null)
                {
                    company.ImageList = BlobStorageEngine.UpdateCompanyImages(model.CompanyImages, company);
                }
                #endregion

                _accessService.UpdateCompany(company);

                return RedirectToAction("Store", "Manage");
            }

            return View(model);
        }

        public ActionResult DeleteStore(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue && id > 0)
                {
                    Company company = _accessService.GetCompanyByID(id.Value);

                    if (company != null)
                    {
                        BlobStorageEngine.DeleteCompanyPhotos(company);

                        _accessService.DeleteFlorist(company.Florist);
                        _accessService.DeleteCompany(company);
                    }
                }
            }

            return RedirectToAction("Store", "Manage");
        }
    }
}
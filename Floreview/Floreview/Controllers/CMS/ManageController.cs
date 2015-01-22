using Floreview.DataAccess.Interfaces;
using Floreview.Models;
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
        #region Fields & Props
        private IAccessService _accessService = null;
        #endregion

        #region Constructor
        public ManageController(IAccessService service)
        {
            _accessService = service;
        }

        public ManageController()
        {

        }
        #endregion

        #region Actions
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
        public ActionResult AddStore(StoreVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Company.Location = _accessService.GetLocationByCityName(model.Company.Location.City);
                    model.Company.Coordinates = DbGeography.FromText("POINT(" + model.LongitudeRAW + " " + model.LatitudeRAW + ")");
                    model.Company.Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
                    model.Company.Florist.ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
                    int id = _accessService.InsertCompany(model.Company);

                    UploadImagesToStorage(model, id);


                    return RedirectToAction("Store", "Manage");
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                System.Diagnostics.Debug.WriteLine("Bad variable");
                //return RedirectToAction("AddStore", "Manage");
                throw;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Server error");
                //return RedirectToAction("AddStore", "Manage");
                throw;
            }
        }

        public ActionResult EditStore(int id)
        {
            try
            {
                StoreVM model = new StoreVM();

                if (ModelState.IsValid)
                {
                    if (id > 0)
                    {
                        Company company = _accessService.GetCompanyByID(id);

                        if (company != null)
                        {
                            model.Company = company;

                            return View(model);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Store", "Manage");
            }
        }

        [HttpPost]
        public ActionResult EditStore(StoreVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model = UploadImagesToStorageEdit(model);

                    Company update = _accessService.GetCompanyByID(model.Company.ID);
                    update.Name = model.Company.Name;
                    update.DescriptionShort = model.Company.DescriptionShort;
                    update.DescriptionLong = model.Company.DescriptionLong;
                    update.Facebook = model.Company.Facebook;
                    update.Website = model.Company.Website;
                    update.Email = model.Company.Email;
                    update.Florist = model.Company.Florist;
                    update.Avatar = model.Company.Avatar;
                    update.ImageList = model.Company.ImageList;
                    update.Location = _accessService.GetLocationByCityName(model.Company.Location.City);
                    update.Address = model.Company.Address;
                    update.Coordinates = DbGeography.FromText("POINT(" + model.LongitudeRAW + " " + model.LatitudeRAW + ")");


                    _accessService.UpdateCompany(update);

                    return RedirectToAction("Store", "Manage");
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                System.Diagnostics.Debug.WriteLine("Bad variable");
                //return RedirectToAction("AddStore", "Manage");
                throw;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Server error");
                //return RedirectToAction("AddStore", "Manage");
                throw;
            }
        }

        public ActionResult DeleteStore(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id > 0)
                    {
                        Company company = _accessService.GetCompanyByID(id);

                        if (company != null)
                        {
                            DeleteImagesFromStorage(company);

                            _accessService.DeleteCompany(company);

                            return RedirectToAction("Store", "Manage");
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                System.Diagnostics.Debug.WriteLine("Bad variable");
                //return RedirectToAction("Store", "Manage");
                throw;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Server error");
                throw;
            }
        }
        #endregion

        #region Functions
        private void UploadImagesToStorage(StoreVM model, int id)
        {
            BlobStorageEngine storageEngine = new BlobStorageEngine();

            #region Company Avatar
            if (model.CompanyAvatar != null)
            {
                // upload picture
                String filename = "profile_" + id + "_store.jpg";
                String companyAvatarURL = storageEngine.UploadImageToStorage(model.CompanyAvatar, filename, "profiles");

                if (!String.IsNullOrEmpty(companyAvatarURL))
                {
                    model.Company.Avatar = companyAvatarURL;
                }
                else
                {
                    // upload failed -> set default avatar;
                    model.Company.Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
                }
            }
            else
            {
                // no image selected -> set default avatar;
                model.Company.Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
            }
            #endregion

            #region Florist Avatar
            if (model.FloristAvatar != null)
            {
                // upload picture
                String filename = "profile_" + id + "_florist.jpg";
                String floristAvatarURL = storageEngine.UploadImageToStorage(model.FloristAvatar, filename, "profiles");

                if (!String.IsNullOrEmpty(floristAvatarURL))
                {
                    model.Company.Florist.ImagePath = floristAvatarURL;
                }
                else
                {
                    model.Company.Florist.ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
                }
            }
            else
            {
                model.Company.Florist.ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
            }
            #endregion

            #region Company Images
            if (model.CompanyImages[0] != null)
            {
                String imageList = "";

                for (int i = 0; i < model.CompanyImages.Length; i++)
                {
                    // upload picture
                    String filename = "profile_" + id + "_image_" + i + ".jpg";
                    String imagePath = storageEngine.UploadImageToStorage(model.CompanyImages[i], filename, "profiles");

                    imageList += imagePath + ";";
                }

                model.Company.ImageList = imageList;
            }
            else
            {
                model.Company.ImageList = null;
            }
            #endregion

            _accessService.UpdateCompany(model.Company);
        }

        private StoreVM UploadImagesToStorageEdit(StoreVM model)
        {
            BlobStorageEngine storageEngine = new BlobStorageEngine();

            #region Company Avatar
            if (model.CompanyAvatar != null)
            {
                // upload picture
                String filename = "profile_" + model.Company.ID + "_store.jpg";
                String companyAvatarURL = storageEngine.UploadImageToStorage(model.CompanyAvatar, filename, "profiles");

                if (!String.IsNullOrEmpty(companyAvatarURL))
                {
                    model.Company.Avatar = companyAvatarURL;
                }
                else
                {
                    // upload failed -> set default avatar;
                    model.Company.Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
                }
            }
            #endregion

            #region Florist Avatar
            if (model.FloristAvatar != null)
            {
                // upload picture
                String filename = "profile_" + model.Company.ID + "_florist.jpg";
                String floristAvatarURL = storageEngine.UploadImageToStorage(model.FloristAvatar, filename, "profiles");

                if (!String.IsNullOrEmpty(floristAvatarURL))
                {
                    model.Company.Florist.ImagePath = floristAvatarURL;
                }
                else
                {
                    model.Company.Florist.ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
                }
            }
            #endregion

            #region Company Images
            if (model.CompanyImages[0] != null)
            {
                List<CloudBlockBlob> lstBlobs = storageEngine.GetAllBlobs("profiles");
                foreach (CloudBlockBlob blob in lstBlobs)
                {
                    if (blob.Name.Contains("profile_" + model.Company.ID + "_image_"))
                    {
                        // delete images related to company
                        storageEngine.DeleteImageFromStorage(blob.Name, "profiles");
                    }
                }

                String imageList = "";

                for (int i = 0; i < model.CompanyImages.Length; i++)
                {
                    // upload pictures
                    String filename = "profile_" + model.Company.ID + "_image_" + i + ".jpg";
                    String imagePath = storageEngine.UploadImageToStorage(model.CompanyImages[i], filename, "profiles");

                    imageList += imagePath + ";";
                }

                model.Company.ImageList = imageList;
            }
            #endregion

            return model;
        }

        private void DeleteImagesFromStorage(Company c)
        {
            BlobStorageEngine storageEngine = new BlobStorageEngine();

            String storeAvatarFilename = "profile_" + c.ID + "_store.jpg";
            String storeFloristFilename = "profile_" + c.ID + "_florist.jpg";

            if (!String.IsNullOrEmpty(c.ImageList))
            {
                String[] split = c.ImageList.Split(';');
                foreach (String image in split)
                {
                    if (!String.IsNullOrEmpty(image))
                    {
                        String[] imageSplit = image.Split('/');
                        storageEngine.DeleteImageFromStorage(imageSplit[4], "profiles");
                    }
                }
            }

            storageEngine.DeleteImageFromStorage(storeAvatarFilename, "profiles");
            storageEngine.DeleteImageFromStorage(storeFloristFilename, "profiles");
        }
        #endregion
    }
}
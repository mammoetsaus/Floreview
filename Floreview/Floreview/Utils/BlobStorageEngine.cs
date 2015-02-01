using Floreview.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Floreview.Utils
{
    public abstract class BlobStorageEngine
    {
        private static CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["BlobStorageConnectionString"].ToString());

        private static CloudBlobClient _blobClient = null;

        private static String UploadImageToStorage(HttpPostedFileBase image, String filename, String targetContainer)
        {
            try
            {
                CloudBlobContainer container = _blobClient.GetContainerReference(targetContainer);

                CloudBlockBlob blob = container.GetBlockBlobReference(filename);

                blob.UploadFromStream(image.InputStream);

                return "http://floreview.blob.core.windows.net/profiles/" + filename;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void DeleteImageFromStorage(String filename, String targetContainer)
        {
            try
            {
                CloudBlobContainer container = _blobClient.GetContainerReference(targetContainer);

                CloudBlockBlob blob = container.GetBlockBlobReference(filename);

                if (blob.Exists())
                {
                    blob.Delete();
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting images from blob storage");
            }
        }

        private static List<CloudBlockBlob> GetAllBlobs(String targetContainer)
        {
            try
            {
                List<CloudBlockBlob> lstBlobs = new List<CloudBlockBlob>();

                CloudBlobContainer container = _blobClient.GetContainerReference(targetContainer);

                foreach (IListBlobItem item in container.ListBlobs(null, false))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;

                        lstBlobs.Add(blob);
                    }
                }

                return lstBlobs;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error selecting blobs in container");
                return null;
            }
        }

        public static String UploadCompanyAvatar(HttpPostedFileBase companyAvatar, Company company)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            String filename = "profile_" + company.ID + "_store.jpg";
            String companyAvatarURL = UploadImageToStorage(companyAvatar, filename, "profiles");

            if (!String.IsNullOrEmpty(companyAvatarURL))
            {
                return companyAvatarURL;
            }

            return "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
        }

        public static String UploadFloristAvatar(HttpPostedFileBase floristAvatar, Company company)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            String filename = "profile_" + company.ID + "_florist.jpg";
            String floristAvatarURL = UploadImageToStorage(floristAvatar, filename, "profiles");

            if (!String.IsNullOrEmpty(floristAvatarURL))
            {
                return floristAvatarURL;
            }

            return "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
        }

        public static String UploadCompanyImages(HttpPostedFileBase[] images, Company company)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            String imagelist = "";

            for (int i = 0; i < images.Length; i++)
            {
                // upload picture
                String filename = "profile_" + company.ID + "_image_" + i + ".jpg";
                String imagePath = UploadImageToStorage(images[i], filename, "profiles");

                imagelist += imagePath;
                imagelist += ";";
            }

            return imagelist;
        }

        public static String UpdateCompanyImages(HttpPostedFileBase[] images, Company company)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            List<CloudBlockBlob> blobs = GetAllBlobs("profiles");

            foreach (CloudBlockBlob blob in blobs)
	        {
                if (blob.Name.Contains("profile_" + company.ID + "_image_"))
                {
                    DeleteImageFromStorage(blob.Name, "profiles");
                }
	        }

            return UploadCompanyImages(images, company);
        }

        public static void DeleteCompanyPhotos(Company company)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            String companyAvatarURL = String.Format("profile_{0}_store.jpg", company.ID);
            String floristAvatarURL = String.Format("profile_{0}_florist.jpg", company.ID);

            if (!String.IsNullOrEmpty(company.ImageList))
            {
                String[] split = company.ImageList.Split(';');
                for (int i = 0; i < split.Length; i++)
                {
                    if (!String.IsNullOrEmpty(split[i]))
                    {
                        String[] imageParts = split[i].Split('/');
                        DeleteImageFromStorage(imageParts[4], "profiles");
                    }
                }
            }

            DeleteImageFromStorage(companyAvatarURL, "profiles");
            DeleteImageFromStorage(floristAvatarURL, "profiles");
        }
    }
}
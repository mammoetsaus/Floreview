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

                return filename;

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
            String companyAvatarFileName = UploadImageToStorage(companyAvatar, filename, "profiles");

            if (!String.IsNullOrEmpty(companyAvatarFileName))
            {
                return "http://floreview.blob.core.windows.net/profiles/" + companyAvatarFileName;
            }

            return "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
        }

        public static String UploadFloristAvatar(HttpPostedFileBase floristAvatar, Company company)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            String filename = "profile_" + company.ID + "_florist.jpg";
            String floristAvatarFileName = UploadImageToStorage(floristAvatar, filename, "profiles");

            if (!String.IsNullOrEmpty(floristAvatarFileName))
            {
                return "http://floreview.blob.core.windows.net/profiles/" + floristAvatarFileName;
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
                String imageFileName = UploadImageToStorage(images[i], filename, "profiles");

                String imageURL = "http://floreview.blob.core.windows.net/profiles/" + imageFileName;

                imagelist += imageURL;
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

        public static String UploadBlogAvatar(HttpPostedFileBase blogAvatar, Blog blog)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            String filename = "blog_" + blog.ID + "_avatar.jpg";
            String blogAvatarFileName = UploadImageToStorage(blogAvatar, filename, "blog");

            if (!String.IsNullOrEmpty(blogAvatarFileName))
            {
                return "http://floreview.blob.core.windows.net/blog/" + blogAvatarFileName;
            }

            return "http://floreview.blob.core.windows.net/blog/blog_avatar_default.jpg";
        }

        public static void DeleteBlogAvatar(Blog blog)
        {
            _blobClient = _storageAccount.CreateCloudBlobClient();

            String blogAvatarURL = String.Format("blog_{0}_avatar.jpg", blog.ID);
            
            DeleteImageFromStorage(blogAvatarURL, "blog");
        }
    }
}
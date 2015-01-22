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
    public class BlobStorageEngine
    {
        #region Fields & Props
        private CloudStorageAccount _storageAccount = null;

        private CloudBlobClient _blobClient = null;
        #endregion

        #region Constructor
        public BlobStorageEngine()
        {
            _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["BlobStorageConnectionString"].ToString());
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }
        #endregion

        #region Functions
        public String UploadImageToStorage(HttpPostedFileBase image, String filename, String targetContainer)
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

        public void DeleteImageFromStorage(String filename, String targetContainer)
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

        public List<CloudBlockBlob> GetAllBlobs(String targetContainer)
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
        #endregion
    }
}
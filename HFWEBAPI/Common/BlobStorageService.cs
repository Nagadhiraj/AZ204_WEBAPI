using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
 
namespace HFWEBAPI.Common
{
    public class BlobStorageService
    {
        string accessKey = string.Empty;
        private IConfiguration _config;

        public BlobStorageService(IConfiguration config)
        {
            _config = config;
            this.accessKey = _config.GetValue<string>("Values:BLOB_ACC_ACCESS_KEY"); // AppConfiguration.GetConfiguration("AccessKey");
        }

        public string UploadFileToBlob(string containerName, string strFileName, byte[] fileData, string fileMimeType)
        {
            try
            {

                var _task = Task.Run(() => this.UploadFileToBlobAsync(containerName, strFileName, fileData, fileMimeType));
                _task.Wait();
                string fileUrl = _task.Result;
                return fileUrl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async void DeleteBlobData(string fileUrl)
        {
            Uri uriObj = new Uri(fileUrl);
            string BlobName = Path.GetFileName(uriObj.LocalPath);

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "uploads";
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);

            string pathPrefix = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/";
            CloudBlobDirectory blobDirectory = cloudBlobContainer.GetDirectoryReference(pathPrefix);
            // get block blob refarence    
            CloudBlockBlob blockBlob = blobDirectory.GetBlockBlobReference(BlobName);

            // delete blob from container        
            await blockBlob.DeleteAsync();
        }


        private async Task<string> UploadFileToBlobAsync(string strContainerName, string fileName, byte[] fileData, string fileMimeType)
        {
            try
            {

                CloudStorageAccount storageAccount;
                if (CloudStorageAccount.TryParse(accessKey, out storageAccount))
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);

                    if (await cloudBlobContainer.CreateIfNotExistsAsync())
                    {
                        await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    }

                    if (fileName != null && fileData != null)
                    {
                        CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                        cloudBlockBlob.Properties.ContentType = fileMimeType;
                        await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                        return cloudBlockBlob.Uri.AbsoluteUri;
                    }
                    return "";
                }
                else
                {
                    return "error";
                }


                //CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
                //CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                ////string strContainerName = "uploads";
                //CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
                ////string fileName = this.GenerateFileName(strFileName);
                //cloudBlobContainer.FetchAttributes();

                //if (await cloudBlobContainer.CreateIfNotExistsAsync())
                //{
                //    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                //}

                //if (fileName != null && fileData != null)
                //{
                //    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                //    cloudBlockBlob.Properties.ContentType = fileMimeType;
                //    await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
                //    return cloudBlockBlob.Uri.AbsoluteUri;
                //}
                //return "";

                //// Create a BlobServiceClient object which will be used to create a container client
                //BlobServiceClient blobServiceClient = new BlobServiceClient(accessKey);
                //var abc = await blobServiceClient.GetPropertiesAsync();
                //BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(strContainerName);
                //await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
                //if (fileName != null && fileData != null)
                //{
                //    BlobClient blobClient = containerClient.GetBlobClient(fileName);
                //    await blobClient.UploadAsync(new MemoryStream(fileData));
                //    // Anonymously access a blob given its URI
                //    Uri endpoint = blobClient.Uri;
                //    BlobClient anonymous = new BlobClient(endpoint);

                //    // Make a service request to verify we've successfully authenticated
                //    var bvf = await anonymous.GetPropertiesAsync();

                //    return endpoint.ToString();
                //}

                //return "";

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
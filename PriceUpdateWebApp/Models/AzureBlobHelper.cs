using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
namespace ArasPLMWebAp.Models
{
    public class AzureBlobHelper
    {
        public CloudBlobClient BlobClient
        {
            get;
            set;
        }
        public CloudBlobContainer BlobContainer
        {
            get;
            set;
        }
        public CloudBlockBlob DownloadBlob(string folderPath, string filename, string containerName = null, string connectionString = null)
        {
            return DownloadBlob(folderPath + "/" + filename, containerName, connectionString);
        }
        public string AddToBlobSTorage(string fundId, string filename, byte[] byteArray, string containerName = null, string connectionString = null)
        {
            GetContainer(containerName, connectionString);
            string blobName = fundId + "/" + filename;
            CloudBlockBlob blockBlob = BlobContainer.GetBlockBlobReference(blobName);
            blockBlob.UploadFromByteArrayAsync(byteArray, 0, byteArray.Length);
            return blobName;
        }
        public string AddToBlobSTorageAsStream(string fundId, string filename, Stream stream, string containerName = null, string connectionString = null)
        {
            return AddToBlobStorageAsStream(fundId + "/" + filename, stream, containerName, connectionString);
        }
        public string AddToBlobStorageAsStream(string filename, Stream stream, string containerName, string connectionString)
        {
            GetContainer(containerName, connectionString);
            CloudBlockBlob blockBlob = BlobContainer.GetBlockBlobReference(filename);
            blockBlob.UploadFromStreamAsync(stream);
            return filename;
        }
        public CloudBlockBlob DownloadBlob(string filename, string containerName, string connectionString)
        {
            GetContainer(containerName, connectionString);
            CloudBlockBlob blockBlob = BlobContainer.GetBlockBlobReference(filename);
            return blockBlob;
        }
        private void GetContainer(string containerName, string connectionString = null)
        {
            containerName = containerName ?? ConfigurationManager.AppSettings["uploadsContainerName"] ?? "uploads";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString ?? ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            BlobClient = storageAccount.CreateCloudBlobClient();
            BlobContainer = BlobClient.GetContainerReference(containerName);
            BlobContainer.CreateIfNotExistsAsync();
        }
        public string GetBlobUrl(string filename, string containerName, string connectionString)
        {
            containerName = containerName ?? ConfigurationManager.AppSettings["uploadsContainerName"] ?? "uploads";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString ?? ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(filename);
            if (!blob.ExistsAsync().Result)
            {
                return null;
            }
            var sign = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = System.DateTimeOffset.UtcNow.AddDays(5),
                SharedAccessStartTime = System.DateTimeOffset.MinValue,
                Permissions = SharedAccessBlobPermissions.Read
            });
            return blob.Uri.AbsoluteUri + sign;
        }
    }
}
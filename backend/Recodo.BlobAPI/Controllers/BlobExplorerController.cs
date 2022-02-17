using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Recodo.BlobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobExplorerController : ControllerBase
    {
        private readonly string connectionString = "";


       [HttpGet]
       public async Task<IActionResult> GetBlobs()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("recodo-test");

            List<Uri> allBlobs = new List<Uri>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var response = await container.ListBlobsSegmentedAsync(blobContinuationToken);

                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        allBlobs.Add(blob.Uri);
                    }
                }
                blobContinuationToken = response.ContinuationToken;
            } while (blobContinuationToken != null);     

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(int id, IFormFile files)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("recodo-test");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(id.ToString());

            using (var fileStream = files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(int id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("recodo-test");

            

            Uri uri = new Uri(id.ToString());
            string filename = Path.GetFileName(uri.LocalPath);

            var blob = container.GetBlockBlobReference(id.ToString());

            await blob.DeleteIfExistsAsync();

            return NoContent();

        }
    }
}

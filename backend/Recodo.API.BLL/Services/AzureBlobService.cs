using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using Recodo.API.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.API.BLL.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;

        public AzureBlobService(IAzureBlobConnectionFactory azureBlobConnectionFactory)
        {
            _azureBlobConnectionFactory = azureBlobConnectionFactory;
        }

		public async Task<IEnumerable<Uri>> ListAsync()
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			var allBlobs = new List<Uri>();
			BlobContinuationToken blobContinuationToken = null;
			do
			{
				var response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
				foreach (IListBlobItem blob in response.Results)
				{
					if (blob.GetType() == typeof(CloudBlockBlob))
						allBlobs.Add(blob.Uri);
				}
				blobContinuationToken = response.ContinuationToken;
			} while (blobContinuationToken != null);
			return allBlobs;
		}

		public async Task UploadAsync(int id, IFormFile files)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			var blob = blobContainer.GetBlockBlobReference(id.ToString());

			using (var fileStream = files.OpenReadStream())
			{
				await blob.UploadFromStreamAsync(fileStream);
			}
		}

		public async Task DeleteAsync(int id)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			var blob = blobContainer.GetBlockBlobReference(id.ToString());

			await blob.DeleteIfExistsAsync();
		}

        public async Task<Stream> DownloadAsync(int id)
        {
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			var blob = blobContainer.GetBlockBlobReference(id.ToString());

			return await blob.OpenReadAsync();
		}
    }
}

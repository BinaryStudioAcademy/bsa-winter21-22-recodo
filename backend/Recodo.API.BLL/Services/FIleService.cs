using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob;
using Recodo.API.BLL.Interfaces;
using Recodo.FIle.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.API.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;
		private readonly IRequestService _requestService;

		public FileService(IAzureBlobConnectionFactory azureBlobConnectionFactory, IRequestService requestService)
        {
            _azureBlobConnectionFactory = azureBlobConnectionFactory;
			_requestService = requestService;
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

		public async Task UploadAsync(IFormFile files, string token)
		{
			var id = await _requestService.SendSaveRequest(token);

			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			var blob = blobContainer.GetBlockBlobReference(id);

			await using var fileStream = files.OpenReadStream();
			await blob.UploadFromStreamAsync(fileStream);

			var response = await _requestService.SendFinishRequest(Convert.ToInt32(id));	
		}

		public async Task DeleteAsync(int id)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			var blob = blobContainer.GetBlockBlobReference(id.ToString());

			await blob.DeleteIfExistsAsync();
		}

        public async Task<Stream> DownloadAsync(int id, string token)
        {
			await _requestService.SendGetRequest(token);

			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			var blob = blobContainer.GetBlockBlobReference(id.ToString());		

			return await blob.OpenReadAsync();
		}
    }
}

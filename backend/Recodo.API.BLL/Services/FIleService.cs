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

		public async Task<string> UploadAsync(IFormFile files, string token)
		{
			var id = await _requestService.SendSaveRequest(token);

			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			var blob = blobContainer.GetBlockBlobReference(id);

			await using var fileStream = files.OpenReadStream();
			await blob.UploadFromStreamAsync(fileStream);

			return await _requestService.SendFinishRequest(Convert.ToInt32(id));	
		}

		public async Task DeleteAsync(int id)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			var blob = blobContainer.GetBlockBlobReference(id.ToString());

			await blob.DeleteIfExistsAsync();
		}

        public async Task<(Stream response, int? errorCode)> DownloadAsync(int id, string token)
        {
			if (await _requestService.SendGetRequest(id, token))
            {
				var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

				var blob = blobContainer.GetBlockBlobReference(id.ToString());

				return (await blob.OpenReadAsync(), null);
			}
			else
            {
				return (null, 403);
            }
		}

		public async Task<string> GetUrlAsync(int id, string token)
        {
			if (await _requestService.SendGetRequest(id, token))
			{
				var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

				var blob = blobContainer.GetBlockBlobReference(id.ToString());

				var uri = blob.Uri.AbsoluteUri;

				return uri;
			}
			return null;
		}
    }
}

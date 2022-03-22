using Recodo.API.BLL.Interfaces;
using Recodo.FIle.BLL.Interfaces;
using System;
using System.IO;
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

		public async Task<string> UploadAsync(Stream files, string token, int id)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			var blob = blobContainer.GetBlockBlobReference(id + ".mp4");

			await using var fileStream = files;
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
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob;
using Recodo.API.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.API.BLL.Services
{
    public class FIleService : IAzureBlobService
    {
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;

		private static HttpClient Client { get; set; } = new HttpClient();

		private static string BaseUrl = new string(@"https://localhost:44316/api/");


		public FIleService(IAzureBlobConnectionFactory azureBlobConnectionFactory)
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

		public async Task UploadAsync(IFormFile files, string token)
		{
			Client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

			var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Post, BaseUrl +
				$"File"));
			if (response.IsSuccessStatusCode == false)
			{
				throw new Exception("Error");
			}
			var id = await response.Content.ReadAsStringAsync();

			//Need to receive videoId from response

			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			var blob = blobContainer.GetBlockBlobReference(id);

			using (var fileStream = files.OpenReadStream())
			{
				await blob.UploadFromStreamAsync(fileStream);
			}

			response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Put, BaseUrl +
				$"File?id={id}"));
			if (response.IsSuccessStatusCode == false)
			{
				throw new Exception("Error");
			}
			//Put id

		}

		public async Task DeleteAsync(int id)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			var blob = blobContainer.GetBlockBlobReference(id.ToString());

			await blob.DeleteIfExistsAsync();
		}

        public async Task<Stream> DownloadAsync(int id)
        {
			var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, BaseUrl +
				$"File?id={id}"));
			if (response.IsSuccessStatusCode == false)
			{
				throw new Exception("Invalid user");
			}


			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			var blob = blobContainer.GetBlockBlobReference(id.ToString());

			return await blob.OpenReadAsync();
		}
    }
}

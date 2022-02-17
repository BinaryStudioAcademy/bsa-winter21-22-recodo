using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    internal class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        const string BlobContainerName = "recodo-filecontainer";       
        private readonly CloudBlobContainer blobContainer;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        

    }
}

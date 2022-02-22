using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.API.BLL.Interfaces
{
    public interface IAzureBlobService
    {
        Task<IEnumerable<Uri>> ListAsync();
        Task UploadAsync(IFormFile files, string token);
        Task<Stream> DownloadAsync(int id);
        Task DeleteAsync(int id);

    }
}

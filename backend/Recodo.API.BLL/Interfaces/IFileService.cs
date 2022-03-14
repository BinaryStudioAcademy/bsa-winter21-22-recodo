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
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile files, string token);
        Task<(Stream response, int? errorCode)> DownloadAsync(int id, string token);
        Task DeleteAsync(int id);

    }
}

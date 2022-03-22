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
        Task<string> UploadAsync(Stream files, string token, int videoId);
        Task<(Stream response, int? errorCode)> DownloadAsync(int id, string token);
        Task DeleteAsync(int id);
        Task<string> GetUrlAsync(int id, string token);
    }
}

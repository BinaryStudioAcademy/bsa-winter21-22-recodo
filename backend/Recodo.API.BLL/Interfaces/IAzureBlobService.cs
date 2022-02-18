using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.API.BLL.Interfaces
{
    public interface IAzureBlobService
    {
        Task<IEnumerable<Uri>> ListAsync();
        Task UploadAsync(int id, IFormFile files);
        Task DeleteAsync(int id);

    }
}

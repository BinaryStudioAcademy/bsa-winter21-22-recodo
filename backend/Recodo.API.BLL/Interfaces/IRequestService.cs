using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.FIle.BLL.Interfaces
{
    public interface IRequestService
    {
        Task<string> SendSaveRequest(string token);
        Task<bool> SendGetRequest(int id ,string token);
        Task<bool> SendFinishRequest(int videoId);
    }
}

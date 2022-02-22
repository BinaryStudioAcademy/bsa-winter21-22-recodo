using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.FIle.BLL.Interfaces
{
    internal interface IRequestService
    {
        Task<string> SendSaveRequest(string token);
        Task SendGetRequest(int id, string token);
        Task SendFinishRequest(int videoId);
    }
}

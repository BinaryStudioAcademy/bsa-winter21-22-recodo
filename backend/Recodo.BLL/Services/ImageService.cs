using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.User;
using Recodo.DAL.Context;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class ImageService : BaseService
    {
        public ImageService(RecodoDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<string> UploadToGyazo(IFormFile avatar, string gyazoKey)
        {
            using (var client = new HttpClient())
            {
                var url = "https://upload.gyazo.com/api/upload?access_token=" + gyazoKey;

                using (var memoryStream = new MemoryStream())
                {
                    await avatar.CopyToAsync(memoryStream);
                    var avatarImage = memoryStream.ToArray();

                    ByteArrayContent byteContent = new ByteArrayContent(avatarImage);
                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(byteContent, "imagedata", "imagedata");

                    var response = await client.PostAsync(url, multipartContent);
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    var json = JsonConvert.DeserializeObject<AvatarDTO>(stringResponse);
                    return json.ThumbUrl;
                }
            }
        }
    }
}

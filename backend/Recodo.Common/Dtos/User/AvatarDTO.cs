using Newtonsoft.Json;

namespace Recodo.Common.Dtos.User
{
    public class AvatarDTO
    {
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
    }
}

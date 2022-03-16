using AutoMapper;
using Recodo.Common.Dtos.User;
using Recodo.Common.Dtos.Video;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoDTO>();
            CreateMap<NewVideoDTO, Video>()
                .ForMember(video => video.Reactions, act => act.Ignore());
        }
    }
}

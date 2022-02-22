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
            CreateMap<VideoDTO, Video>();
            CreateMap<Video, VideoDTO>();
        }
    }
}

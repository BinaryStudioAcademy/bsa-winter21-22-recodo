using AutoMapper;
using Recodo.Common.Dtos;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class VideoProfiles : Profile
    {
        public VideoProfiles()
        {
            CreateMap<VideoReaction, VideoReactionDTO>();
            CreateMap<VideoReactionDTO, VideoReaction>();
            CreateMap<Video, VideoDTO>();
        }
    }
}

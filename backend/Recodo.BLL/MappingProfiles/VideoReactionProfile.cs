using AutoMapper;
using Recodo.Common.Dtos.Reactions;
using Recodo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.BLL.MappingProfiles
{
    public class VideoReactionProfile : Profile
    {
        public VideoReactionProfile()
        {
            CreateMap<NewVideoReactionDTO, VideoReaction>();
            CreateMap<VideoReaction, VideoReactionDTO>();
        }
    }
}

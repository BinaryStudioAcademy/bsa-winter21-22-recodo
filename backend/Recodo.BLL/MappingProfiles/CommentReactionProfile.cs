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
    public class CommentReactionProfile : Profile
    {
        public CommentReactionProfile()
        {
            CreateMap<NewCommentReactionDTO, CommentReaction>();
            CreateMap<CommentReaction, CommentReactionDTO>();
        }
    }
}

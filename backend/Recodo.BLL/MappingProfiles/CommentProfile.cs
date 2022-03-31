using AutoMapper;
using Recodo.Common.Dtos.Comment;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<NewCommentDTO, CommentDTO>();
            CreateMap<NewCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}

using AutoMapper;
using Recodo.Common.Dtos.Comment;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<NewCommentDTO, Comment>()
                .ForMember(comment => comment.AuthorId, conf => conf.MapFrom(arg => arg.UserId));
            CreateMap<Comment, CommentDTO>();
        }
    }
}

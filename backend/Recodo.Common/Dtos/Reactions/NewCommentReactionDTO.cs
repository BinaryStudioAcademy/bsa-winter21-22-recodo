using Recodo.Common.Enums;

namespace Recodo.Common.Dtos.Reactions 
{
    public class NewCommentReactionDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public Reaction Reaction { get; set; }
    }
}
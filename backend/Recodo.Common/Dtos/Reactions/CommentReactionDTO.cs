using Recodo.Common.Enums;

namespace Recodo.Common.Dtos.Reactions 
{
    public class CommentReactionDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public Reaction Reaction { get; set; }
    }
}
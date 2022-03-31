using Recodo.Common.Enums;

namespace Recodo.Common.Dtos.Reactions 
{
    public class NewVideoReactionDTO
    {
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public Reaction Reaction { get; set; }
    }
}
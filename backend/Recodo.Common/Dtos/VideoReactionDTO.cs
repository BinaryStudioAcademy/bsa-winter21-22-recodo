using Recodo.Common.Enums;
using System;

namespace Recodo.Common.Dtos
{
    public class VideoReactionDTO
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Reaction Reaction { get; set; }
    }
}

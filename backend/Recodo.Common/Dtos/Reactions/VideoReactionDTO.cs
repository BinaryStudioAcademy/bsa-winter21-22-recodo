using Recodo.Common.Enums;
using System;

namespace Recodo.Common.Dtos.Reactions 
{
    public class VideoReactionsDTO
    {
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public Reaction Reaction { get; set; }
    }
}
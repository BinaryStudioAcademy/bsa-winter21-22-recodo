using Recodo.Common.Dtos.User;
using System;
using System.Collections.Generic;
using Recodo.Common.Dtos.Reactions;

namespace Recodo.Common.Dtos.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CommentReactionDTO> Reactions { get; set; }
        public string Body { get; set; }
    }
}
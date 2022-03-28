using Recodo.Common.Dtos.User;
using System;
using System.Collections.Generic;
using Recodo.Common.Dtos.Reactions;

namespace Recodo.Common.Dtos.Comment
{
    public class NewCommentDTO
    {
        public int AuthorId { get; set; }
        public int VideoId { get; set; }
        public string Body { get; set; }
        public ICollection<NewCommentReactionDTO> Reactions { get; set; }
    }
}
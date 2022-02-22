using Recodo.Common.Dtos.User;
using System;
using System.Collections.Generic;
using Recodo.Common.Dtos.Reactions;

namespace Recodo.Common.Dtos.Comment
{
    public class NewCommentDTO
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string Body { get; set; }
    }
}
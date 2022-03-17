using System;
using Recodo.Common.Dtos.User;
using System.Collections.Generic;
using Recodo.Common.Dtos.Reactions;
using Recodo.Common.Dtos.Comment;

namespace Recodo.Common.Dtos.Video
{
    public class VideoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public UserDTO Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FolderId { get; set; }
        public ICollection<VideoReactionDTO> Reactions { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
    }
}
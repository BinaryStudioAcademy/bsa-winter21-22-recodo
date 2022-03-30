using Recodo.Common.Dtos.Comment;
using System;
using System.Collections.Generic;
using Recodo.Common.Dtos.Comment;


namespace Recodo.Common.Dtos
{
    public class VideoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FolderId { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public ICollection<VideoReactionDTO> Reactions { get; set; }
        public List<string> SharedEmails { get; set; }
    }
}

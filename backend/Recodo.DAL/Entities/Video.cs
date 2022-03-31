using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool IsSaving { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? FolderId { get; set; }
        public List<VideoReaction> Reactions { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

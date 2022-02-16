using Recodo.Common.Dtos.User;
using System.Collections.Generic;

namespace Recodo.Common.Dtos.Folder
{
    public class FolderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public UserDTO Author { get; set; }
        public FolderDTO Parent { get; set; }
        public int? ParentId { get; set; }
        public int TeamId { get; set; }
        public ICollection<FolderDTO> SubFolders { get; } = new List<FolderDTO>();
    }
}

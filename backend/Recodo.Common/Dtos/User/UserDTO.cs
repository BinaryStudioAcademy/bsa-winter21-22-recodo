
using System.Collections.Generic;

namespace Recodo.Common.Dtos.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string WorkspaceName { get; set; }
        public string AvatarLink { get; set; }
        public List<TeamDTO> Teams { get; set; } = null;
    }
}

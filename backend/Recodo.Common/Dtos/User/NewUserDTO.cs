using System.ComponentModel.DataAnnotations;

namespace Recodo.Common.Dtos.User
{
    public class NewUserDTO : UserDTO
    {
        [Required]
        public string Password { get; set; }
    }
}

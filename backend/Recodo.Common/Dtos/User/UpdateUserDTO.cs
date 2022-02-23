﻿namespace Recodo.Common.Dtos.User
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string WorkspaceName { get; set; }
        public string AvatarLink { get; set; } = "";
        public string PasswordNew { get; set; }
        public string PasswordCurrent { get; set; }
        public string Token { get; set; }
    }
}

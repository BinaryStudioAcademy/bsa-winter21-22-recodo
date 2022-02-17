﻿using AutoMapper;
using Recodo.Common.Dtos.User;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NewUserDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}

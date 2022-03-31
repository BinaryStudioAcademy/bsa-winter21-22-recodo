using AutoMapper;
using Recodo.Common.Dtos.Access;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class AccessProfile : Profile
    {
        public AccessProfile()
        {
            CreateMap<AccessForRegisteredUsers, AccessForRegisteredUsersDTO>();
            CreateMap<AccessForRegisteredUsersDTO, AccessForRegisteredUsers>();
            CreateMap<AccessForUnregisteredUsers, AccessForUnregisteredUsersDTO>();
            CreateMap<AccessForUnregisteredUsersDTO, AccessForUnregisteredUsers>();
        }
    }
}

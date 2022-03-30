using AutoMapper;
using Recodo.Common.Dtos;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDTO>()
                .ForMember(m => m.MemberCount, opt => opt.MapFrom(src => src.Users.Count));
        }
    }
}

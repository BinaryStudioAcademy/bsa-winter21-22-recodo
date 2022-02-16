using AutoMapper;
using Recodo.Common.Dtos.Folder;
using Recodo.DAL.Entities;

namespace Recodo.BLL.MappingProfiles
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<NewFolderDTO, Folder>();
            CreateMap<Folder, FolderDTO>();
        }
    }
}

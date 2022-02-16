using AutoMapper;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Folder;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Threading.Tasks;


namespace Recodo.BLL.Services
{
    class FolderService : BaseService
    {
        public FolderService(RecodoDbContext context, IMapper mapper) : base (context, mapper)
        {
        }

        public async Task<FolderDTO> CreateFolder(NewFolderDTO newFolderDTO)
        {
            var folderEntity = _mapper.Map<Folder>(newFolderDTO);

            _context.Folders.Add(folderEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<FolderDTO>(folderEntity);
        }
    }
}

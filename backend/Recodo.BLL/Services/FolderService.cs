using AutoMapper;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Folder;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Recodo.BLL.Exceptions;

namespace Recodo.BLL.Services
{
    public class FolderService : BaseService
    {
        public FolderService(RecodoDbContext context, IMapper mapper) : base (context, mapper)
        {
        }

        public async Task<FolderDTO> Create(NewFolderDTO newFolderDTO)
        {
            var folderEntity = _mapper.Map<Folder>(newFolderDTO);

            _context.Folders.Add(folderEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<FolderDTO>(folderEntity);
        }

        public async Task<List<FolderDTO>> GetFoldersByAuthorId(int authorId)
        {
            var foldersEntities = await _context.Folders.
                Where(f => f.AuthorId == authorId).
                ToListAsync();

            if(foldersEntities.Count == 0)
            {
                throw new NotFoundException(nameof(Folder));
            }

            var folders = _mapper.Map<List<FolderDTO>>(foldersEntities);

            return folders;
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new NotFoundException(nameof(Folder), id);

            var folderEntity = await _context.Folders.FirstOrDefaultAsync(f => f.Id == id);

            if(folderEntity is null)
                throw new NotFoundException(nameof(Folder), id);

            _context.Folders.Remove(folderEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FolderDTO folder)
        {
            var folderEntity = await _context.Folders.FirstOrDefaultAsync(f => f.Id == folder.Id);

            if (folderEntity is null)
                throw new NotFoundException(nameof(Folder), folder.Id);

            folderEntity.Name = folder.Name;
            folderEntity.AuthorId = folder.AuthorId;
            folderEntity.TeamId = folder.TeamId;

            _context.Folders.Update(folderEntity);
            await _context.SaveChangesAsync();
        }
    }
}

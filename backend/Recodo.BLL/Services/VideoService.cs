using AutoMapper;
using Recodo.BLL.Services.Abstract;
using Recodo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recodo.Common.Dtos;
using Microsoft.EntityFrameworkCore;


namespace Recodo.BLL.Services
{
    public class VideoService : BaseService
    {
        public VideoService(RecodoDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public async Task<List<VideoDTO>> GetVideosByFolderId(int folderId)
        {
            var videoEntities = await _context.Videos.AsNoTracking()
                .Include(video => video.Reactions)
                .Where(v => v.FolderId == folderId)
                .ToListAsync();

            var videos = _mapper.Map<List<VideoDTO>>(videoEntities);

            return videos;
        }

        public async Task<List<VideoDTO>> GetVideosByUserIdWithoutFolder(int userId)
        {
            var videoEntities = await _context.Videos.AsNoTracking()
                .Include(video => video.Reactions)
                .Where(v => v.AuthorId == userId && v.FolderId == null)
                .ToListAsync();

            var videos = _mapper.Map<List<VideoDTO>>(videoEntities);

            return videos;
        }
    }
}

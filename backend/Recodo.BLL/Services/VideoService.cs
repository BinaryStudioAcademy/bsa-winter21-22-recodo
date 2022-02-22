using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Comment;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Collections.Generic;
using Recodo.Common.Dtos.Video;

namespace Recodo.BLL.Services
{
    public sealed class VideoService : BaseService
    {
        public VideoService(RecodoDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ICollection<VideoDTO>> GetVideos()
        {
            var allVideos = await _context.Videos.ToListAsync();
            return _mapper.Map<List<VideoDTO>>(allVideos);
        }

        public async Task<VideoDTO> GetVideoById(int id)
        {
            var foundVideo = await _context.Videos.FindAsync(id);
            return _mapper.Map<VideoDTO>(foundVideo);
        }
    }
}

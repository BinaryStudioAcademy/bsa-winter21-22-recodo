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
using Recodo.BLL.Exceptions;
using Recodo.DAL.Entities;
using Microsoft.Extensions.Configuration;

namespace Recodo.BLL.Services
{
    public class VideoService : BaseService
    {
        private readonly IConfiguration _configuration;
        public VideoService(RecodoDbContext context, IMapper mapper, IConfiguration configuration) : base(context, mapper)
        {
            _configuration = configuration;
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

        public async Task<bool> CheckVideoState(int id)
        {
            var videoEntity = await _context.Videos.FirstOrDefaultAsync(v => v.Id == id);

            if(videoEntity is null)
            {
                throw new NotFoundException(nameof(Video), id);
            }

            return videoEntity.IsSaving;
        }

        public async Task Delete(int videoId)
        {
            var videoEntity = await _context.Videos.FirstOrDefaultAsync(v => v.Id == videoId);

            if(videoEntity is null)
            {
                throw new NotFoundException(nameof(Video), videoId);
            }

            _context.Videos.Remove(videoEntity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(VideoDTO video)
        {
            var foundVideo = await _context.Videos.FirstOrDefaultAsync(v => v.Id == video.Id);
            if (foundVideo is null)
            {
                throw new NotFoundException(nameof(Video), foundVideo.Id);
            }
            foundVideo.IsPrivate = video.IsPrivate;
            _context.Videos.Update(foundVideo);
            await _context.SaveChangesAsync();
        }

        public async Task<VideoDTO> GetVideoById(int id)
        {
            var videoEntity = await _context.Videos.AsNoTracking()
                .Include(video => video.Reactions)
                .Where(v => v.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<VideoDTO>(videoEntity);
        }

        public async Task SendEmail(string body, string email, string name = "")
        {
            await EmailService.SendEmailAsync(email, name, body, _configuration);
        }
    }
}

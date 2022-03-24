using AutoMapper;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Comment;
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
using Recodo.Common.Dtos.Video;

namespace Recodo.BLL.Services
{
    public class VideoService : BaseService
    {
        private readonly CommentService _commentService;
        public VideoService(RecodoDbContext context, IMapper mapper, CommentService commentService) : base(context, mapper) {
            this._commentService = commentService;
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
            var videoComments = _commentService.GetAllVideosComments(foundVideo.Id);
            videoEntity.Comments = _mapper.Map<List<Comment>>(videoComments);
            return _mapper.Map<VideoDTO>(videoEntity);
        }

        public async Task<VideoDTO> AddVideo (NewVideoDTO newVideo)
        {
            var video = _mapper.Map<Video>(newVideo);
            video.CreatedAt = DateTime.Now;
            video.Reactions = _mapper.Map<List<VideoReaction>>(newVideo.Reactions);
            await _context.Videos.AddAsync(video);
            _context.SaveChanges();
            return _mapper.Map<VideoDTO>(video);
        }
    }
}

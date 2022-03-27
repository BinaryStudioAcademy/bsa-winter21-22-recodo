using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Comment;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Collections.Generic;
using Recodo.Common.Dtos.Video;
using System;

namespace Recodo.BLL.Services
{
    public sealed class VideoService : BaseService
    {
        private readonly CommentService _commentService;
        public VideoService(RecodoDbContext context, IMapper mapper, CommentService commentService) : base(context, mapper) {
            _commentService = commentService;
         }

        public async Task<ICollection<VideoDTO>> GetVideos()
        {
            var allVideos = await _context.Videos.ToListAsync();
            return _mapper.Map<List<VideoDTO>>(allVideos);
        }

        public async Task<VideoDTO> GetVideoById(int id)
        {
            var videoEntity = await _context.Videos
                .Include(video => video.Reactions)
                .FirstOrDefaultAsync(video => video.Id == id);
            var videoDto = _mapper.Map<VideoDTO>(videoEntity);
            var videoComments = await _commentService.GetAllVideosComments(videoDto.Id);
            videoDto.Comments = videoComments;
            return videoDto;
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

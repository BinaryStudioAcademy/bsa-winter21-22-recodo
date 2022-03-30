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
using System.IdentityModel.Tokens.Jwt;
using Recodo.API.Middleware;

namespace Recodo.BLL.Services
{
    public class VideoService : BaseService
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
        public async Task Delete(int videoId, string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var authorId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;

            if (authorId == null)
            {
                throw new Exception("Can not get user id from token");
            }

            var videoEntity = await _context.Videos.FirstOrDefaultAsync(v => v.Id == videoId);

            if(videoEntity is null)
            {
                throw new NotFoundException(nameof(Video), videoId);
            }
            if (videoEntity.AuthorId != Convert.ToInt32(authorId))
            {
                throw new Exception("No access to file");
            }

            _context.Videos.Remove(videoEntity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(UpdateVideoDTO videoDTO)
        {
            var videoEntity = await _context.Videos.FirstOrDefaultAsync(v => v.Id == videoDTO.Id);

            if(videoEntity is null)
            {
                throw new NotFoundException(nameof(Video), videoDTO.Id);
            }

            videoEntity.Name = videoDTO.Name;

            _context.Videos.Update(videoEntity);
            await _context.SaveChangesAsync();
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

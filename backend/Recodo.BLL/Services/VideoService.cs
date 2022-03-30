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
using System.IdentityModel.Tokens.Jwt;

namespace Recodo.BLL.Services
{
    public class VideoService : BaseService
    {
        private EmailService _emailService;
        public VideoService(RecodoDbContext context, IMapper mapper, EmailService emailService) : base(context, mapper)
        {
            _emailService = emailService;
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
            var videoEntity = await _context.Videos.AsNoTracking()
                .Include(video => video.Reactions)
                .Where(v => v.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<VideoDTO>(videoEntity);
        }

        public async Task SendEmail(string body, string email, string name = "")
        {
            await _emailService.SendEmailAsync(email, "Shared video", body, name);
        }
    }
}

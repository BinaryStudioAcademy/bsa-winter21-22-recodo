using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.Blob;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class FileService
    {
        private readonly RecodoDbContext _context;

        public FileService(RecodoDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveVideo(int authorId)
        {
            Video newVideo = new Video()
            {
                IsSaving = true,
                AuthorId = authorId,
                CreatedAt = DateTime.Now,
                Name = $"Video_{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}_{DateTime.Now.Hour}{DateTime.Now.Minute}"
            };

            await _context.AddAsync(newVideo);
            await _context.SaveChangesAsync();

            return newVideo.Id;
        }

        public async Task FinishLoadingFile(int id)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video == null)
            {
                throw new Exception("Video with such identifier is not found.");
            }

            video.IsSaving = false;
        }
        public async Task GetFile(int userId, int videoId)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(x => x.Id == videoId);
            if (video == null)
            {
                throw new Exception("Video with such identifier is not found.");
            }
            if (video.AuthorId != userId)
            {
                throw new Exception("User doesn't have acces to this video.");
            }            
        }

    }
}

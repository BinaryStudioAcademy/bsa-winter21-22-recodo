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
            Video newVideo = new Video();
            newVideo.IsSaving = true;
            newVideo.AuthorId = authorId;
            newVideo.CreatedAt = DateTime.Now;
            newVideo.Name = "Video";

            await _context.AddAsync(newVideo);
            await _context.SaveChangesAsync();

            return newVideo.Id;
        }

        public async Task FinishLoadingFile(int id)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video == null)
            {
                throw new Exception("Invalid id");
            }

            video.IsSaving = false;
        }
    }
}

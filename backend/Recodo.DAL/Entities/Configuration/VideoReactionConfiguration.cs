using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities.Configuration
{
    public class VideoReactionConfiguration : IEntityTypeConfiguration<VideoReaction>
    {
        public void Configure(EntityTypeBuilder<VideoReaction> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne<Video>().WithMany().HasForeignKey(p => p.VideoId);
            builder.HasOne<User>().WithMany().HasForeignKey(p => p.UserId);
        }
    }
}

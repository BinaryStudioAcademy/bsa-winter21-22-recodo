﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Body).IsRequired();
            builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).IsRequired();
            builder.HasOne<Video>().WithMany().HasForeignKey(p => p.VideoId);
        }
    }
}

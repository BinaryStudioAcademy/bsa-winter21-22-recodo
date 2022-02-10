using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities.Configuration
{
    public class CommentReactionConfiguration : IEntityTypeConfiguration<CommentReaction>
    {
        public void Configure(EntityTypeBuilder<CommentReaction> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p =>p .User).WithMany().HasForeignKey(p => p.UserId);
            builder.Property(p => p.Reaction).IsRequired();
            builder.HasOne<Comment>().WithMany().HasForeignKey(p => p.CommentId);
        }
    }
}

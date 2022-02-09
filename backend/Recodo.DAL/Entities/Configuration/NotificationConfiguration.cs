using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Massage).IsRequired();
            builder.Property(p=>p.ReceiverId).IsRequired();
            builder.HasOne<User>().WithMany().HasForeignKey(p => p.ReceiverId);
        }
    }
}

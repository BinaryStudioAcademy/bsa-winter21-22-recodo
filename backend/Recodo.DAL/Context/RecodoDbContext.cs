using Microsoft.EntityFrameworkCore;
using Recodo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Context
{
    public class RecodoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Team> Teams  { get; set; }
        public DbSet<VideoReaction> VideoReactions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TeamInvitation> TeamInvitations { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public RecodoDbContext(DbContextOptions<RecodoDbContext> options) : base(options)
        {


        }
    }
}

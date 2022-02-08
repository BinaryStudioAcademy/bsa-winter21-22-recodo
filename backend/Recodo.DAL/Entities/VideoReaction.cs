﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities
{
    public class VideoReaction
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public Reaction Reaction { get; set; }
    }
}

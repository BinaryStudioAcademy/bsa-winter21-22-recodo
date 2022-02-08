﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities
{
    public class InviteToTeam
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAccepted { get; set; }
    }
}

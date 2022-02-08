﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.DAL.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Massege { get; set; }
        public NotificationType Type { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public enum NotificationType
        {
            BillingNotification,
            NewVideoNotification,
            NewReactionNotification,
            NewComment
        }
    }
}

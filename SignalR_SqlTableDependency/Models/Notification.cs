using System;
using System.Collections.Generic;

namespace SignalR_SqlTableDependency.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string MessageType { get; set; } = null!;
        public DateTime NotificationDateTime { get; set; }
    }
}
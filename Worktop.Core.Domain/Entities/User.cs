using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Worktop.Core.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public int? JobId { get; set; }
        public float SalaryBonus { get; set; } = 1f;
        public DateTime? DateHired { get; set; }

        public Job Job { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<TaskItem> Tasks { get; set; } = new HashSet<TaskItem>();
        public ICollection<Opinion> Opinions { get; set; } = new HashSet<Opinion>();
        public ICollection<Mail> MailsSent { get; set; } = new HashSet<Mail>();
        public ICollection<Mail> MailsReceived { get; set; } = new HashSet<Mail>();
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
        public ICollection<Connection> Connections { get; set; } = new HashSet<Connection>();
        public ICollection<ChatRoom> ChatRooms { get; set; } = new HashSet<ChatRoom>();
        public ICollection<File> Files { get; set; } = new HashSet<File>();
        public ICollection<Directory> Directories { get; set; } = new HashSet<Directory>();

        public static User Create(string email, string username, DateTime? dateHired = null) => new User
        {
            Email = email,
            UserName = username,
            DateHired = dateHired ?? DateTime.Now
        };
    }
}
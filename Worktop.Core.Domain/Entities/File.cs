using System;
using Worktop.Core.Common.Helpers;

namespace Worktop.Core.Domain.Entities
{
    public class File
    {
        public string Id { get; set; } = Utils.Id();
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public uint Size { get; set; }
        public string DirectoryId { get; set; }
        public int? UserId { get; set; }

        public Directory Directory { get; set; }
        public User User { get; set; }
    }
}
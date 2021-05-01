using System;
using Worktop.Core.Helpers;

namespace Worktop.Models.Domain
{
    public class File
    {
        public string Id { get; set; } = Utils.Id();
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public uint Size { get; set; }
        public string DirectoryId { get; set; }
        public int? UserId { get; set; }

        public Directory Directory { get; set; }
        public User User { get; set; }
    }
}
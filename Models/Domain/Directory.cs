using System;
using System.Collections.Generic;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.Models.Domain
{
    public class Directory
    {
        public string Id { get; set; } = Utils.Id();

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxDirectoryLength)]
        [CharactersNotAllowedValidator('#')]
        public string Name { get; set; }

        public string Path { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        public string ParentDirectoryId { get; set; }

        public User User { get; set; }
        public Directory ParentDirectory { get; set; }

        public ICollection<File> Files { get; set; } = new HashSet<File>();
        public ICollection<Directory> Subdirectories { get; set; } = new HashSet<Directory>();
    }
}
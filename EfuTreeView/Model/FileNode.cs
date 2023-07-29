using System;
using System.IO;

namespace EfuTreeView.Model
{
    public class FileNode : IFileTreeNode
    {
        public string Name { get; set; } = "";

        private FileAttributes? _type;
        public FileAttributes? Type { get => _type; set => _type = value & ~FileAttributes.Directory; }

        public ulong? Size { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateCreated { get; set; }

        public override string ToString() => $"{Name} {Type}";
    }
}
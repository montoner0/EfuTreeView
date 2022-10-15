using System;
using System.IO;

namespace EfuTreeView.Model
{
    public class FileNode : IFileTreeNode
    {
        public string Name { get; set; } = "";

        public FileAttributes Type { get; set; }

        public ulong? Size { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public override string ToString() => $"{Name} {Type}";
    }
}
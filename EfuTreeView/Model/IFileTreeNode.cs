using System;
using System.IO;

namespace EfuTreeView.Model
{
    public interface IFileTreeNode
    {
        public string Name { get; }
        public FileAttributes Type { get; }
        public DateTime DateModified { get; }
        public DateTime DateCreated { get; }
    }
}

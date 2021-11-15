using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace EfuTreeView
{
    public class FileNode : IFileTreeNode
    {
        //public string FullPath { get; set; }
        public string Name { get; set; }
        public FileAttributes Type { get; set; }
        //public ObservableCollection<IFileTreeNode> Nodes { get; set; }
        public ulong? Size { get; set; }
        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public override string ToString() => $"{Name} {Type}";
    }
}
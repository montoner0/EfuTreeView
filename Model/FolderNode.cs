using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace EfuTreeView.Model
{
    public class FolderNode : IFileTreeNode
    {
        public string Name { get; set; }
        public FileAttributes Type => FileAttributes.Directory;
        public List<IFileTreeNode> Nodes { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public override string ToString() => $"{Name} {Type} {Nodes?.Count}";
    }
}
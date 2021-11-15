using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace EfuTreeView
{
    public interface IFileTreeNode
    {
        //public string FullPath { get; }
        public string Name { get; }
        public FileAttributes Type { get; }
        public DateTime DateModified { get; }
        public DateTime DateCreated { get; }
        //public ObservableCollection<IFileTreeNode> Nodes { get; }
    }
}

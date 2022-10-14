using System;
using System.IO;

namespace EfuTreeView.ViewModel
{
    public class NodeData
    {
        public string Name { get; set; } = "";
        public ulong? Size { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public FileAttributes Attributes { get; set; }
    }
}

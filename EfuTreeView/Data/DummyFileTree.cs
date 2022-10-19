using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EfuTreeView.Helpers;
using EfuTreeView.Model;

namespace EfuTreeView
{
    public class DummyFileTree : IFileTreeBuilder
    {
        private readonly List<IFileTreeNode> _fileTree = new()
        {
            new FolderNode
            {
                Name ="C:",
                Nodes = new()
                {
                    new FileNode { Name="pagefile.sys", Size=(ulong)8.GiB(), DateModified=new DateTime(2011,12,15) },
                    new FileNode { Name="hiberfil.sys", Size=(ulong)5.GiB(), DateModified=new DateTime(2011,12,15) },
                    new FolderNode
                    {
                        Name ="Acme",
                        Nodes = new()
                        {
                            new FileNode { Name="acme.exe", Size=(ulong)15.MiB(), DateModified=new DateTime(2001,1,5) },
                            new FileNode { Name="acme.dll", Size=(ulong)1.MiB(), DateModified=new DateTime(2001,1,5) },
                            new FileNode { Name="readme.txt", Size=(ulong)1.KiB(), DateModified=new DateTime(2002,10,2) },
                            new FileNode { Name="acme.png", Size=(ulong)15.KiB(), DateModified=new DateTime(2001,10,4) },
                        }
                    }
                }
            },
            new FolderNode
            {
                Name ="D:",
                Nodes = new()
                {
                    new FileNode { Name="image.jpg", Size=(ulong)5.MiB(), DateModified=new DateTime(2017,10,3) },
                    new FileNode { Name="music.mp3", Size=(ulong)15.MiB(), DateModified=new DateTime(2014,4,3) },
                    new FileNode { Name ="movie.mp4", Size=(ulong)3.4.GiB(), DateModified=new DateTime(2017,7,9) }
                }
            },
            new FileNode { Name="VD", Type=FileAttributes.ReadOnly|FileAttributes.Hidden },
            new FileNode { Name="VD2" },
            new FileNode { Name="VD3" }
        };

        public IEnumerable<IFileTreeNode> GetNode(string? nodePath = null)
        {
            if (nodePath == null) return _fileTree;

            var pathParts = nodePath.Split("\\");

            var fileTree = _fileTree;

            //var folder = (FolderNode?)fileTree.SingleOrDefault(n => n is FolderNode && n.Name.Equals(pathParts![0], StringComparison.OrdinalIgnoreCase));

            foreach (var part in pathParts) {
                var folder = (FolderNode?)fileTree.SingleOrDefault(n => n is FolderNode && n.Name.Equals(part, StringComparison.OrdinalIgnoreCase));
                if (folder == null) break;
                fileTree = folder.Nodes;
            }

            return fileTree;
        }
    }
}

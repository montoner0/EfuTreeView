using System;
using System.Collections.ObjectModel;
using EfuTreeView.Helpers;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class TreeViewModelOld : ViewModelBase
    {
        private ObservableCollection<IFileTreeNode> _nodes;

        public TreeViewModelOld()
        {
            Load();
        }

        public ObservableCollection<IFileTreeNode> Nodes
        {
            get => _nodes;
            set {
                _nodes = value;
                RaisePropertyChanged();
            }
        }

        public void Load() => Nodes = new ObservableCollection<IFileTreeNode>
            {
                new FolderNode
                {
                    Name ="C:",
                    Nodes = new ObservableCollection<IFileTreeNode>
                    {
                        new FileNode {Name="pagefile.sys", Size=(ulong)8.GB(), DateModified=new DateTime(2011,12,15)},
                        new FileNode {Name="hiberfil.sys", Size=(ulong)5.GB(), DateModified=new DateTime(2011,12,15) },
                        new FolderNode
                        {
                            Name ="Acme",
                            Nodes = new ObservableCollection<IFileTreeNode>
                            {
                                new FileNode {Name="acme.exe", Size=(ulong)15.MB(), DateModified=new DateTime(2001,1,5) },
                                new FileNode {Name="acme.dll", Size=(ulong)1.MB(), DateModified=new DateTime(2001,1,5) },
                                new FileNode {Name="readme.txt", Size=(ulong)1.KB(), DateModified=new DateTime(2002,10,2) },
                                new FileNode {Name="acme.png", Size=(ulong)15.KB(), DateModified=new DateTime(2001,10,4) },
                            }
                        }
                    }
                },
                new FolderNode
                {
                    Name ="D:",
                    Nodes = new ObservableCollection<IFileTreeNode>
                    {
                        new FileNode {Name="image.jpg", Size=(ulong)5.MB(), DateModified=new DateTime(2017,10,3) },
                        new FileNode {Name="music.mp3", Size=(ulong)15.MB(), DateModified=new DateTime(2014,4,3) },
                        new FileNode { Name ="movie.mp4", Size=(ulong)3.4.GB(), DateModified=new DateTime(2017,7,9) }
                    }
                },
                new FileNode { Name="VD" },
                new FileNode { Name="VD2" },
                new FileNode { Name="VD3" }
            };

        public void Load(string filePath)
        {
            filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            var efu = EfuParser.BuildEfuData(filePath);
            Nodes = new ObservableCollection<IFileTreeNode>(efu.GetNodes());
        }
    }
}

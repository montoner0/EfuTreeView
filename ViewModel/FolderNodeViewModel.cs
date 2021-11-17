using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class FolderNodeViewModel : TreeItemViewModel
    {
        private readonly FolderNode _folder;
        protected readonly ObservableCollection<ITreeItemViewModel> _nodes;

        public FolderNodeViewModel(FolderNodeViewModel parent, FolderNode folder) : base(parent, folder.Name)
        {
            _folder = folder;
            _nodes = folder.Nodes is null
                ? null
                : new ObservableCollection<ITreeItemViewModel>(folder.Nodes.Select(n => n switch {
                    FileNode file => new FileNodeViewModel(this, file),
                    FolderNode dir => new FolderNodeViewModel(this, dir) as ITreeItemViewModel,
                    _ => throw new ArgumentException(nameof(folder.Nodes))
                }));
        }

        public override ObservableCollection<ITreeItemViewModel> Nodes => _nodes;
    }
}

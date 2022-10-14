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
        protected readonly ObservableCollection<ITreeItemViewModel>? _nodes;

        public FolderNodeViewModel(IList<IFileTreeNode> nodes) : base(null, null) => _nodes = PopulateNodes(nodes);

        public FolderNodeViewModel(FolderNodeViewModel parent, FolderNode folder) :
            base(parent, new NodeData {
                Name = folder.Name,
                DateCreated = folder.DateCreated,
                DateModified = folder.DateModified
            }) => _nodes = PopulateNodes(folder.Nodes);

        public override ObservableCollection<ITreeItemViewModel>? Nodes => _nodes;
        private ObservableCollection<ITreeItemViewModel>? PopulateNodes(IList<IFileTreeNode> nodes)
        {
            return nodes is null
                    ? null
                    : new ObservableCollection<ITreeItemViewModel>(nodes.Select(n => n switch {
                        FileNode file => new FileNodeViewModel(this, file),
                        FolderNode dir => new FolderNodeViewModel(this, dir) as ITreeItemViewModel,
                        _ => throw new ArgumentException("Unknown node type", nameof(nodes))
                    }));
        }
    }
}

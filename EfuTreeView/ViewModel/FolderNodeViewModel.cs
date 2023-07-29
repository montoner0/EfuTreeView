using System;
using System.Collections.Generic;
using System.Linq;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class FolderNodeViewModel : TreeItemViewModel
    {
        private readonly IFileTree _fileTreeBuilder;

        public FolderNodeViewModel(IFileTree fileTreeBuilder) : base(null, null)
        {
            _fileTreeBuilder = fileTreeBuilder;
            foreach (var item in PopulateNodes(_fileTreeBuilder.GetNode())) {
                Nodes.Add(item);
            }
        }

        private FolderNodeViewModel(FolderNodeViewModel parent, FolderNode folder, IFileTree fileTreeBuilder) :
            base(parent, new NodeData {
                Name = folder.Name,
                DateCreated = folder.DateCreated,
                DateModified = folder.DateModified
            })
        {
            Nodes.Add(Dummy);
            _fileTreeBuilder = fileTreeBuilder;
        }

        protected override void LoadChildren()
        {
            foreach (var item in PopulateNodes(_fileTreeBuilder.GetNode(CurrentPath))) {
                Nodes.Add(item);
            }
        }

        private IEnumerable<ITreeItemViewModel> PopulateNodes(IEnumerable<IFileTreeNode> nodes)
        {
            return nodes.Select(n => n switch {
                       FileNode file => new FileNodeViewModel(this, file),
                       FolderNode dir => new FolderNodeViewModel(this, dir, _fileTreeBuilder) as ITreeItemViewModel,
                       _ => throw new ArgumentException("Unknown node type", $"{n.GetType()}")
                   });
        }

        protected override SelectedInfo GetSelectedInfo()
        {
            var si = base.GetSelectedInfo();
            si.Size = "";
            return si;
        }
    }
}

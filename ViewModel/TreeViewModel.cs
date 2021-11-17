using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EfuTreeView.Helpers;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class TreeViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ITreeItemViewModel> _rootNodes;

        public TreeViewModel(IList<IFileTreeNode> children)
        {
            _rootNodes = new ObservableCollection<ITreeItemViewModel>(children.Select(c => c switch {
                FileNode file => new FileNodeViewModel(null, file),
                FolderNode dir => new FolderNodeViewModel(null, dir) as ITreeItemViewModel,
                _ => throw new ArgumentException(nameof(children))
            }));
        }

        public ObservableCollection<ITreeItemViewModel> RootNodes
        {
            get => _rootNodes;
            //set {
            //    _rootNodes = value;
            //    RaisePropertyChanged();
            //}
        }

        internal void Load(string fileName) => throw new NotImplementedException();
    }
}

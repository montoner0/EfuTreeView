using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EfuTreeView.Helpers;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class TreeItemViewModel : ViewModelBase, ITreeItemViewModel
    {
        private string _selected;
        private bool _isSelected;
        private readonly ITreeItemViewModel _parent;
        private readonly NodeData _nodeData;

        public TreeItemViewModel(ITreeItemViewModel parent, NodeData nodeData)
        {
            _parent = parent;
            _nodeData = nodeData;
        }

        public string SelectedInfo
        {
            get => _selected;
            set {
                if (_parent == null) {
                    if (_selected != value) {
                        _selected = value;
                        RaisePropertyChanged();
                    }
                } else
                    _parent.SelectedInfo = value;
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set {
                if (_isSelected != value) {
                    _isSelected = value;
                    RaisePropertyChanged();
                    if (_isSelected) {
                        SelectedInfo = $"{NodeInfo}";
                    }
                }
            }
        }

        public virtual string NodeInfo =>
            $"{_nodeData.Size.GetValueOrDefault().FormatBytes(false),20} | {_nodeData.DateModified} | {_nodeData.DateCreated}{(_nodeData.Attributes == 0 ? "" : $" | {_nodeData.Attributes}")}";

        public ITreeItemViewModel Parent => _parent;

        public virtual ObservableCollection<ITreeItemViewModel> Nodes => null;

        public string Name => _nodeData.Name;

        protected virtual void LoadChildren() { }
    }
}

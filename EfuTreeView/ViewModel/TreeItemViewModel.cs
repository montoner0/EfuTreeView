using System.Collections.ObjectModel;
using System.Linq;
using EfuTreeView.Helpers;

namespace EfuTreeView.ViewModel
{
    public class TreeItemViewModel : ViewModelBase, ITreeItemViewModel
    {
        private string? _selected;
        private bool _isSelected;
        private bool _isExpanded;
        private readonly NodeData? _nodeData;

        protected static ITreeItemViewModel _dummy { get; } = new TreeItemViewModel(null, new() { Name = "Loading Data ..." });

        public TreeItemViewModel(ITreeItemViewModel? parent, NodeData? nodeData)
        {
            Parent = parent;
            _nodeData = nodeData;
        }

        public string? SelectedInfo
        {
            get => _selected;
            set {
                if (Parent == null) {
                    if (_selected != value) {
                        _selected = value;
                        RaisePropertyChanged();
                    }
                } else {
                    Parent.SelectedInfo = value;
                }
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

        public bool IsExpanded
        {
            get => _isExpanded;
            set {
                if (_isExpanded != value) {
                    _isExpanded = value;
                    RaisePropertyChanged();
                    if (_isExpanded && HasDummyChild) {
                        LoadChildren();
                        Nodes?.Remove(_dummy);
                    }
                }
            }
        }

        public bool HasDummyChild => Nodes?.Any(n => n == _dummy) == true;

        public virtual string NodeInfo =>
            $"{_nodeData?.Size.GetValueOrDefault().FormatBytes(false),20} | {_nodeData?.DateModified} | {_nodeData?.DateCreated}{(_nodeData?.Attributes == 0 ? "" : $" | {_nodeData?.Attributes}")}";

        public ITreeItemViewModel? Parent { get; }

        public virtual ObservableCollection<ITreeItemViewModel> Nodes { get; } = new();

        public string Name => _nodeData?.Name ?? "";

        public NodeData NodeData => _nodeData ?? new();

        public string CurrentPath =>
            string.IsNullOrEmpty(Parent?.CurrentPath) ? _nodeData?.Name ?? "" : $"{Parent.CurrentPath}\\{_nodeData?.Name}";

        protected virtual void LoadChildren() { }
    }
}

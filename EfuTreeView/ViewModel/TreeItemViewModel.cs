using System.Collections.ObjectModel;
using System.Linq;
using EfuTreeView.Helpers;

namespace EfuTreeView.ViewModel
{
    public class TreeItemViewModel : ViewModelBase, ITreeItemViewModel
    {
        private SelectedInfo _selected = new();
        private bool _isSelected;
        private bool _isExpanded;
        protected readonly NodeData? _nodeData;

        public TreeItemViewModel(ITreeItemViewModel? parent, NodeData? nodeData)
        {
            Parent = parent;
            _nodeData = nodeData;
        }

        public SelectedInfo SelectedInfo
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
                        SelectedInfo = GetSelectedInfo();
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
                        Nodes?.Remove(Dummy);
                    }
                }
            }
        }

        protected static ITreeItemViewModel Dummy { get; } = new TreeItemViewModel(null, new() { Name = "Loading Data ..." });

        private bool HasDummyChild => Nodes?.Any(n => n == Dummy) == true;

        public ITreeItemViewModel? Parent { get; }

        public virtual ObservableCollection<ITreeItemViewModel> Nodes { get; } = new();

        public string Name => _nodeData?.Name ?? "?";

        public string CurrentPath =>
            string.IsNullOrEmpty(Parent?.CurrentPath) ? _nodeData?.Name ?? "" : $"{Parent.CurrentPath}\\{_nodeData?.Name}";

        protected virtual void LoadChildren() { }

        protected virtual SelectedInfo GetSelectedInfo()
        {
            return new() {
                Name = Name,
                Path = CurrentPath,
                Size = _nodeData?.Size.GetValueOrDefault().FormatBytes(false) ?? "?",
                DateCreated = _nodeData?.DateCreated?.ToString() ?? "?",
                DateModified = _nodeData?.DateModified?.ToString() ?? "?",
                Attributes = _nodeData?.Attributes == null ? "?" : _nodeData.Attributes == 0 ? "" : $"{_nodeData?.Attributes}"
            };
        }
    }
}

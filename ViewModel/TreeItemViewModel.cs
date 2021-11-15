using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EfuTreeView.ViewModel
{
    public class TreeItemViewModel : ViewModelBase
    {
        private string _selected;
        private bool _isSelected;
        private readonly TreeItemViewModel _parent;
        private readonly ObservableCollection<TreeItemViewModel> _nodes;

        public TreeItemViewModel(TreeItemViewModel parent)
        {
            _parent = parent;
            _nodes = parent.Nodes;
        }

        public string SelectedInfo
        {
            get => _selected;
            set {
                if (_selected != value) {
                    _selected = value;
                    RaisePropertyChanged();
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
                        //SelectedItem = this;
                        //SelectedInfo=$"{this.}"
                    }
                }
            }
        }

        //public string Name => _parent.Name;
        public TreeItemViewModel Parent => _parent;

        public ObservableCollection<TreeItemViewModel> Nodes => _nodes;

        protected virtual void LoadChildren()
        {
        }
    }
}

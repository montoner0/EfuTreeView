using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class TreeItemViewModel : ViewModelBase, ITreeItemViewModel
    {
        private string _selected;
        private bool _isSelected;
        private readonly ITreeItemViewModel _parent;
        private readonly string _name;

        public TreeItemViewModel(ITreeItemViewModel parent, string name)
        {
            _parent = parent;
            _name = name;
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
        public ITreeItemViewModel Parent => _parent;

        public virtual ObservableCollection<ITreeItemViewModel> Nodes => null;

        public string Name => _name;

        protected virtual void LoadChildren() { }
    }
}

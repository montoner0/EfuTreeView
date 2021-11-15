using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EfuTreeView.ViewModel
{
    public class FolderNodeViewModel : TreeItemViewModel
    {
        private readonly FolderNode _folder;

        public FolderNodeViewModel(FolderNode folder, FolderNodeViewModel parent) : base(parent)
        {
            _folder = folder;
        }

        public string Name => _folder.Name;
    }
}

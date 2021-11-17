using System;
using System.Collections.Generic;
using System.Text;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class FileNodeViewModel : TreeItemViewModel
    {
        private readonly FileNode _file;

        public FileNodeViewModel(FolderNodeViewModel parent, FileNode file) : base(parent, file.Name)
        {
            _file = file;
        }
    }
}

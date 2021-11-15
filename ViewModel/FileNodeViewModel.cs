using System;
using System.Collections.Generic;
using System.Text;

namespace EfuTreeView.ViewModel
{
    public class FileNodeViewModel : TreeItemViewModel
    {
        private readonly FileNode _file;

        public FileNodeViewModel(FileNode file, FolderNodeViewModel parent) : base(parent)
        {
            _file = file;
        }

        public string Name => _file.Name;
    }
}

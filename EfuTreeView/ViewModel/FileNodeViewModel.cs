using System;
using EfuTreeView.Model;

namespace EfuTreeView.ViewModel
{
    public class FileNodeViewModel : TreeItemViewModel
    {
        public FileNodeViewModel(FolderNodeViewModel parent, FileNode file) :
            base(parent, new NodeData {
                Name = file.Name,
                DateCreated = file.DateCreated,
                DateModified = file.DateModified,
                Size = file.Size,
                Attributes = file.Type
            })

        { }
    }
}

using System;
using System.IO;

namespace EfuTreeView.ViewModel
{
    public class SelectedInfo
    {
        public string Name { get; set; } = "?";
        public string Path { get; set; } = "?";
        public string Size { get; set; } = "?";
        public string DateModified { get; set; } = "?";
        public string DateCreated { get; set; } = "?";
        public string Attributes { get; set; } = "?";
    }
}

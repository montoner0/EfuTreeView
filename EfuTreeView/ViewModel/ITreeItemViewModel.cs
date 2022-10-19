using System.Collections.ObjectModel;

namespace EfuTreeView.ViewModel
{
    public interface ITreeItemViewModel
    {
        ITreeItemViewModel? Parent { get; }
        string CurrentPath { get; }
        ObservableCollection<ITreeItemViewModel>? Nodes { get; }
        string Name { get; }
        string? SelectedInfo { get; set; }
        NodeData NodeData { get; }
    }
}
using System.Collections.ObjectModel;

namespace EfuTreeView.ViewModel
{
    public interface ITreeItemViewModel
    {
        ITreeItemViewModel? Parent { get; }
        string CurrentPath { get; }
        ObservableCollection<ITreeItemViewModel>? Nodes { get; }
        string Name { get; }
        SelectedInfo SelectedInfo { get; set; }
    }
}
using System.Collections.ObjectModel;

namespace EfuTreeView.ViewModel
{
    public interface ITreeItemViewModel
    {
        ObservableCollection<ITreeItemViewModel> Nodes { get; }
        string Name { get; }
        string SelectedInfo { get; set; }
    }
}
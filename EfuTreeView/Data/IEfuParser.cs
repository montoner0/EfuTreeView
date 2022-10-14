using System.Collections.Generic;
using EfuTreeView.Model;

namespace EfuTreeView
{
    public interface IEfuParser
    {
        IList<IFileTreeNode> GetNodes();
    }
}
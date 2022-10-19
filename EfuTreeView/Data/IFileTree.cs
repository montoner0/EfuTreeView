using System;
using System.Collections.Generic;
using System.Linq;
using EfuTreeView.Model;

namespace EfuTreeView
{
    public interface IFileTree
    {
        IEnumerable<IFileTreeNode> GetNode(string? nodePath = null);
    }
}

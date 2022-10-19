using System;
using System.Collections.Generic;
using System.Linq;
using EfuTreeView.Model;

namespace EfuTreeView
{
    public interface IFileTreeBuilder
    {
        IEnumerable<IFileTreeNode> GetNode(string? nodePath = null);
    }
}

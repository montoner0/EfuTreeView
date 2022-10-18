using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using EfuTreeView.Model;

namespace EfuTreeView
{
    public sealed class EfuParser : IEfuParser
    {
        private readonly List<IFileTreeNode> _treeData;

        private EfuParser(List<IFileTreeNode> data) => _treeData = data;

        public static EfuParser BuildEfuData(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = Encoding.UTF8, });
            csv.Context.RegisterClassMap<EfuItemMap>();
            var efuItems = csv.GetRecords<EfuItem>();
            return new EfuParser(BuildTree(efuItems));
        }

        public IList<IFileTreeNode> GetNodes() => _treeData;

        private static List<IFileTreeNode> BuildTree(IEnumerable<EfuItem> items)
        {
            return new List<IFileTreeNode>(
                       items.GroupBy(s => s.Path.Split("\\")[0])
                            .Select(g => {
                                var efu = g.FirstOrDefault();
                                return efu?.Attributes.HasFlag(FileAttributes.Directory) == true
                                        ? new FolderNode {
                                            Name = g.Key,
                                            DateCreated = efu.DateCreated ?? default,
                                            DateModified = efu.DateModified ?? default,
                                            Nodes = BuildTree(g.Where(efu => efu.Path.Contains(g.Key) && efu.Path.Length > g.Key.Length + 1)
                                                               .Select(efu => { efu.Path = efu.Path[(g.Key.Length + 1)..]; return efu; }))
                                        }
                                        : new FileNode {
                                            Name = g.Key,
                                            DateCreated = efu?.DateCreated ?? default,
                                            DateModified = efu?.DateModified ?? default,
                                            Type = efu?.Attributes ?? 0,
                                            Size = efu?.Size
                                        } as IFileTreeNode;
                            }));
        }
    }
}

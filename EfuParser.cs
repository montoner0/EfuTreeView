using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using EfuTreeView.ViewModel;

namespace EfuTreeView
{
    public class EfuParser
    {
        private readonly ObservableCollection<IFileTreeNode> _treeData = new ObservableCollection<IFileTreeNode>();
        private readonly IOrderedEnumerable<EfuItem> _efuItems;

        public static EfuParser BuildEfuData(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = System.Text.Encoding.UTF8, });
            csv.Context.RegisterClassMap<EfuItemMap>();
            var efuItems = csv.GetRecords<EfuItem>();
            return new EfuParser(efuItems);

            //var lines = await File.ReadAllTextAsync(filePath, System.Text.Encoding.UTF8).ConfigureAwait(false);

            //return new EfuParser(lines);
        }

        public ObservableCollection<IFileTreeNode> GetNodes()
        {
            return _treeData;
        }

        public EfuParser(ObservableCollection<IFileTreeNode> data)
        {
            _treeData = data;
        }

        private EfuParser(IEnumerable<EfuItem> efuItems)
        {
            _treeData = BuildTree(efuItems);
        }

        private ObservableCollection<IFileTreeNode> BuildTree(IEnumerable<string> strings)
        {
            return new ObservableCollection<IFileTreeNode>(
              from s in strings
              let split = s.Split("/")
              group s by s.Split("/")[0] into g  // Group by first component (before /)
              select new FolderNode {
                  Name = g.Key,
                  Nodes = BuildTree(            // Recursively build children
                                      from s in g
                                      where s.Length > g.Key.Length + 1
                                      select s.Substring(g.Key.Length + 1)) // Select remaining components
              }
              );
        }

        private ObservableCollection<IFileTreeNode> BuildTree(IEnumerable<EfuItem> items)
        {
            return new ObservableCollection<IFileTreeNode>(
                       items.GroupBy(s => s.Filename.Split("\\")[0])
                            .Select(g => {
                                var efu = g.FirstOrDefault();
                                return efu?.Attributes.HasFlag(FileAttributes.Directory) == true
                                        ? new FolderNode {
                                            Name = g.Key,
                                            DateCreated = efu.DateCreated ?? default,
                                            DateModified = efu.DateModified ?? default,
                                            Nodes = BuildTree(g.Where(efu => efu.Filename.Length > g.Key.Length + 1)
                                                                .Select(efu => { efu.Filename = efu.Filename[(g.Key.Length + 1)..]; return efu; }))
                                        }
                                        : new FileNode {
                                            Name = g.Key,
                                            DateCreated = efu?.DateCreated ?? default,
                                            DateModified = efu?.DateModified ?? default,
                                            Type = efu?.Attributes ?? 0
                                        } as IFileTreeNode;
                            }));
        }
    }
}

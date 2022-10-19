using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using EfuTreeView.Model;

namespace EfuTreeView
{
    public sealed class FileTreeBuilder : IFileTreeBuilder
    {
        private readonly IEnumerable<EfuItem> _efuItems;

        private FileTreeBuilder(IEnumerable<EfuItem> data) => _efuItems = data;

        public static FileTreeBuilder BuildFromStream(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = Encoding.UTF8 });
            csv.Context.RegisterClassMap<EfuItemMap>();
            var efuItems = csv.GetRecords<EfuItem>();
            return new FileTreeBuilder(efuItems.ToList());
        }

        public static FileTreeBuilder BuildFromEfuFile(string filePath, IFileSystem fileSystem)
        {
            if (fileSystem is null) {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            using var stream = fileSystem.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return BuildFromStream(stream);
        }

        public IEnumerable<IFileTreeNode> GetNode(string? nodePath = null)
        {
            nodePath = nodePath?.Trim();
            IEnumerable<EfuItem> items = string.IsNullOrEmpty(nodePath)
                ? _efuItems
                : _efuItems.Where(e => e.Path.StartsWith(nodePath, StringComparison.OrdinalIgnoreCase) && e.Path.Length > nodePath.Length);

            return items.GroupBy(e => e.Path[(nodePath?.Length + 1 ?? 0)..].Split("\\")[0]).Select(g => {
                var efu = g.First();
                return efu.Attributes.HasFlag(FileAttributes.Directory) || efu.Path != $"{nodePath}\\{g.Key}"
                        ? new FolderNode {
                            Name = g.Key,
                            Path = efu.Path,
                            DateCreated = efu.DateCreated ?? default,
                            DateModified = efu.DateModified ?? default,
                        }
                        : new FileNode {
                            Name = g.Key,
                            DateCreated = efu.DateCreated ?? default,
                            DateModified = efu.DateModified ?? default,
                            Type = efu.Attributes,
                            Size = efu.Size
                        } as IFileTreeNode;
            });
        }
    }
}

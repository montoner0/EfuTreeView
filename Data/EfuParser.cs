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
using EfuTreeView.Helpers;
using EfuTreeView.Model;

namespace EfuTreeView
{
    public class EfuParser
    {
        private readonly List<IFileTreeNode> _treeData = new List<IFileTreeNode>();
        private readonly IOrderedEnumerable<EfuItem> _efuItems;

        public static EfuParser BuildEfuData(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = Encoding.UTF8, });
            csv.Context.RegisterClassMap<EfuItemMap>();
            var efuItems = csv.GetRecords<EfuItem>();
            return new EfuParser(BuildTree(efuItems));

            //var lines = await File.ReadAllTextAsync(filePath, System.Text.Encoding.UTF8).ConfigureAwait(false);

            //return new EfuParser(lines);
        }

        public IList<IFileTreeNode> GetNodes()
        {
            return _treeData;
        }

        public EfuParser(List<IFileTreeNode> data)
        {
            _treeData = data;
        }

        private static List<IFileTreeNode> BuildTree(IEnumerable<EfuItem> items)
        {
            return new List<IFileTreeNode>(
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
        public static IList<IFileTreeNode> LoadDummyData() => new List<IFileTreeNode>
            {
                new FolderNode
                {
                    Name ="C:",
                    Nodes = new List<IFileTreeNode>
                    {
                        new FileNode { Name="pagefile.sys", Size=(ulong)8.GB(), DateModified=new DateTime(2011,12,15) },
                        new FileNode { Name="hiberfil.sys", Size=(ulong)5.GB(), DateModified=new DateTime(2011,12,15) },
                        new FolderNode
                        {
                            Name ="Acme",
                            Nodes = new List<IFileTreeNode>
                            {
                                new FileNode { Name="acme.exe", Size=(ulong)15.MB(), DateModified=new DateTime(2001,1,5) },
                                new FileNode { Name="acme.dll", Size=(ulong)1.MB(), DateModified=new DateTime(2001,1,5) },
                                new FileNode { Name="readme.txt", Size=(ulong)1.KB(), DateModified=new DateTime(2002,10,2) },
                                new FileNode { Name="acme.png", Size=(ulong)15.KB(), DateModified=new DateTime(2001,10,4) },
                            }
                        }
                    }
                },
                new FolderNode
                {
                    Name ="D:",
                    Nodes = new List<IFileTreeNode>
                    {
                        new FileNode { Name="image.jpg", Size=(ulong)5.MB(), DateModified=new DateTime(2017,10,3) },
                        new FileNode { Name="music.mp3", Size=(ulong)15.MB(), DateModified=new DateTime(2014,4,3) },
                        new FileNode { Name ="movie.mp4", Size=(ulong)3.4.GB(), DateModified=new DateTime(2017,7,9) }
                    }
                },
                new FileNode { Name="VD" },
                new FileNode { Name="VD2" },
                new FileNode { Name="VD3" }
            };

        //public void Load(string filePath)
        //{
        //    filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        //    var efu = BuildEfuData(filePath);
        //    Nodes = new ObservableCollection<IFileTreeNode>(efu.GetNodes());
        //}
        private List<IFileTreeNode> BuildTree(IEnumerable<string> strings)
        {
            return new List<IFileTreeNode>(
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
    }
}

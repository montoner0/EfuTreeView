using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace EfuTreeView
{
    public class EfuItem
    {
        public string Filename { get; set; } = "";
        public ulong? Size { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public FileAttributes Attributes { get; set; }

        public override string ToString() => $"{Filename} {Size} {Attributes} {DateModified} {DateCreated}";
    }
    public class EfuItemMap : ClassMap<EfuItem>
    {
        public EfuItemMap()
        {
            Map(m => m.Filename);
            Map(m => m.Size);
            Map(m => m.Attributes);
            Map(m => m.DateModified).Convert(d => long.TryParse(d.Row.GetField("Date Modified"), out var date)
                ? DateTime.FromFileTime(date)
                : null as DateTime?);
            Map(m => m.DateCreated).Convert(d => long.TryParse(d.Row.GetField("Date Created"), out var date)
                ? DateTime.FromFileTime(date)
                : null as DateTime?);
        }
    }
}

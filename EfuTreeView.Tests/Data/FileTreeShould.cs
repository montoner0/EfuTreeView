using System;
using System.IO.Abstractions;
using System.Text;
using FluentAssertions;
using Moq;

namespace EfuTreeView.Tests
{
    public class FileTreeShould
    {
        private readonly FileTree _testClass;
        private readonly MemoryStream _fileStream;
        private readonly Mock<IFileSystem> _fileSystem;
        private readonly string _csvData = @"Filename,Size,Date Modified,Date Created,Attributes
""C:\ARCH\!dos\UNPACK\AUTOH_II\FILE_ID.DIZ"",136,124228557160000000,,0
""C:\ARCH\UniExtract2\bin\file"",,131218716436078096,,16
""C:\ARCH\UniExtract2\bin\file\bin\file.exe"",45056,128861543380000000,,0
""D:\farplugs\S&R\SRPlugins\Dir2File"",,132703357043273896,,16
""D:\farplugs\S&R\SRPlugins\Dir2File\dir2file.hlf"",4044,127495782320000000,,0
""C:\Perl\html\lib\AnyDBM_File.html"",5645,129692004306255162,,0
""C:\Perl\html\lib\DBD\File"",,129692004340847141,,16
""C:\Perl\html\lib\DBD\File.html"",31887,129692004340167102,,0
""C:\Perl\html\lib\DBI\Profile.html"",50215,129692004360158246,,0
""C:\Perl\html\lib\DBI\ProfileData.html"",21867,129692004360358257,,0
""C:\Perl\html\lib\DBI\ProfileDumper.html"",12283,129692004360488264,,0
""C:\Perl\html\lib\DBI\ProfileSubs.html"",1472,129692004360648274,,0
";

        public FileTreeShould()
        {
            using var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(_csvData));
            _testClass = FileTree.BuildFromStream(fileStream);

            _fileStream = new MemoryStream(Encoding.UTF8.GetBytes(_csvData));

            _fileSystem = new Mock<IFileSystem>();
            _fileSystem.Setup(f => f.File.Open(It.IsRegex(@"^\S.+"), FileMode.Open, FileAccess.Read, FileShare.Read)).Returns(_fileStream);
            _fileSystem.Setup(f => f.File.Open(It.Is<string>(s => string.IsNullOrWhiteSpace(s)), FileMode.Open, FileAccess.Read, FileShare.Read))
                       .Throws<ArgumentNullException>();
        }

        [Fact]
        public void Can_Call_BuildFromStream()
        {
            // Arrange
            var fileStream = _fileStream;

            // Act
            var result = FileTree.BuildFromStream(fileStream);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Cannot_Call_BuildFromStream_WithNull_FileStream()
        {
            FluentActions.Invoking(() => FileTree.BuildFromStream(default(Stream)!)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Can_Call_BuildFromEfuFile()
        {
            // Arrange
            var filePath = "TestValue1428598609";
            var fileDataSource = _fileSystem;

            // Act
            var result = FileTree.BuildFromEfuFile(filePath, fileDataSource.Object);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Cannot_Call_BuildFromEfuFile_WithNull_FileDataSource()
        {
            FluentActions.Invoking(() => FileTree.BuildFromEfuFile("TestValue1887281688", default(IFileSystem)!)).Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Cannot_Call_BuildFromEfuFile_WithInvalid_FilePath(string value)
        {
            FluentActions.Invoking(() => FileTree.BuildFromEfuFile(value, _fileSystem.Object)).Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("C:\\ARCH", 2, 0)]
        [InlineData("C:\\ARCH\\!dos", 1, 0)]
        [InlineData("D:\\farplugs\\S&R\\SRPlugins", 1, 0)]
        [InlineData("C:\\ARCH\\UniExtract2\\bin\\file", 1, 0)]
        [InlineData("C:\\Perl\\html\\lib", 2, 1)]
        [InlineData("C:\\Perl\\html\\lib\\DBI", 0, 4)]
        [InlineData("F:", 0, 0)]
        [InlineData(null, 2, 0)]
        public void Can_Call_GetNode(string? nodePath, int dirsCount, int filesCount)
        {
            // Act
            var result = _testClass.GetNode(nodePath);

            // Assert
            result.Where(n=> n.Type.HasFlag(FileAttributes.Directory)).Should().HaveCount(dirsCount);
            result.Where(n=> !n.Type.HasFlag(FileAttributes.Directory)).Should().HaveCount(filesCount);
        }

        //[Theory]
        //[InlineData("")]
        //[InlineData("   ")]
        //public void Cannot_Call_GetNode_WithInvalid_NodePath(string value)
        //{
        //    FluentActions.Invoking(() => _testClass.GetNode(value)).Should().Throw<ArgumentNullException>();
        //}
    }
}
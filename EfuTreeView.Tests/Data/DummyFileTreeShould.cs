using EfuTreeView;
using System;
using Xunit;
using FluentAssertions;

namespace EfuTreeView.Tests
{
    public class DummyFileTreeShould
    {
        private readonly DummyFileTree _testClass;

        public DummyFileTreeShould()
        {
            _testClass = new DummyFileTree();
        }

        [Theory]
        [InlineData(null, 2, 3)]
        [InlineData("c:", 1, 2)]
        [InlineData("c:\\pagefile.sys", 1, 2)]
        [InlineData("c:\\acme", 0, 4)]
        public void Can_Call_GetNode(string? path, int dirsCount, int filesCount)
        {
            // Act
            var result = _testClass.GetNode(path);

            // Assert
            result.Where(n => n.Type.HasFlag(FileAttributes.Directory)).Should().HaveCount(dirsCount);
            result.Where(n => !n.Type.HasFlag(FileAttributes.Directory)).Should().HaveCount(filesCount);
        }
    }
}
using System;
using EfuTreeView.Helpers;
using FluentAssertions;

namespace EfuTreeView.Tests
{
    public static class NumericExtensionsShould
    {
        public static TheoryData<object> Types =>
            new() {
                0,
                (long)0,
                (uint)0,
                (ulong)0,
                (short)0,
                (ushort)0,
                0m,
                (double)0,
                (float)0,
                (sbyte)0,
                (byte)0
            };

        public static TheoryData<decimal, string, string> Data4Formatting =>
            new() {
                {0                                  ,"0 B"              ,"0 B"   },
                {1000                               ,"1 KB"             ,"1000 B" },
                {1024                               ,$"{1.024:0.###} KB","1 KiB" },
                {1000_000                           ,"1 MB"             ,$"{976.563:0.###} KiB" },
                {1024*1024                          ,$"{1.049:0.###} MB","1 MiB" },
                {1000_000_000                       ,"1 GB"             ,$"{953.674:0.###} MiB" },
                {1024*1024*1024                     ,$"{1.074:0.###} GB","1 GiB" },
                {1000_000_000_000                   ,"1 TB"             ,$"{931.323:0.###} GiB" },
                {1024m*1024*1024*1024               ,$"{1.1:0.###} TB"  ,"1 TiB" },
                {1000_000_000_000_000               ,"1 PB"             ,$"{909.495:0.###} TiB" },
                {1024m*1024*1024*1024*1024          ,$"{1.126:0.###} PB","1 PiB" },
                {1000_000_000_000_000_000           ,"1 EB"             ,$"{888.178:0.###} PiB" },
                {1024m*1024*1024*1024*1024*1024     ,$"{1.153:0.###} EB","1 EiB" },
                {1000_000_000_000_000_000_000m      ,"1 ZB"             ,$"{867.362:0.###} EiB" },
                {1024m*1024*1024*1024*1024*1024*1024,$"{1.181:0.###} ZB","1 ZiB" },
            };

        [Theory]
        [MemberData(nameof(Types))]
        public static void Can_Call_FormatBytes_On_Numeric_Type(dynamic o)
        {
            // Act
            string result = NumericExtensions.FormatBytes(o);

            // Assert
            result.Should().Be("0 B");
        }

        [Fact]
        public static void Cannot_Call_FormatBytes_On_NonNumeric_Type()
        {
            // Assert
            FluentActions.Invoking(()=>NumericExtensions.FormatBytes('.')).Should().Throw<ArgumentException>();
        }

        [Theory]
        [MemberData(nameof(Types))]
        public static void Can_Call_IsNumericType(object o)
        {
            // Act
            var result = o.GetType().IsNumericType();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public static void Can_Call_KB()
        {
            // Arrange
            var kb = 1;

            // Act
            var result = kb.KB();

            // Assert
            result.Should().Be(1000);
        }

        [Fact]
        public static void Can_Call_MB()
        {
            // Arrange
            var mb = 1;

            // Act
            var result = mb.MB();

            // Assert
            result.Should().Be(1000_000);
        }

        [Fact]
        public static void Can_Call_GB()
        {
            // Arrange
            var gb = 1;

            // Act
            var result = gb.GB();

            // Assert
            result.Should().Be(1000_000_000);
        }

        [Fact]
        public static void Can_Call_TB()
        {
            // Arrange
            var tb = 1;

            // Act
            var result = tb.TB();

            // Assert
            result.Should().Be(1000_000_000_000);
        }

        [Theory]
        [MemberData(nameof(Data4Formatting))]
        public static void Can_Call_FormatBytes(decimal bytes, string answerSi, string answerIec)
        {
            // Act
            var resultSi = bytes.FormatBytes(Iec: false);
            var resultIec = bytes.FormatBytes(Iec: true);

            // Assert
            resultSi.Should().Be(answerSi);
            resultIec.Should().Be(answerIec);
        }

        [Fact]
        public static void Can_Call_KiB()
        {
            // Arrange
            var kb = 1;

            // Act
            var result = kb.KiB();

            // Assert
            result.Should().Be(1024);
        }

        [Fact]
        public static void Can_Call_MiB()
        {
            // Arrange
            var mb = 1;

            // Act
            var result = mb.MiB();

            // Assert
            result.Should().Be(1024*1024);
        }

        [Fact]
        public static void Can_Call_GiB()
        {
            // Arrange
            var gb = 1;

            // Act
            var result = gb.GiB();

            // Assert
            result.Should().Be(1024 * 1024 * 1024);
        }

        [Fact]
        public static void Can_Call_TiB()
        {
            // Arrange
            var tb = 1;

            // Act
            var result = tb.TiB();

            // Assert
            result.Should().Be(1024m * 1024 * 1024 * 1024);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace EfuTreeView.Helpers
{
    public static class NumericExtensions
    {
        public static string FormatBytes<T>(this T bytes, bool Iec = true)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            if (typeof(T).IsNumericType()) {
                var bytes2 = Convert.ToDecimal(bytes);
                var divisor = Iec ? 1024 : 1000;
                var prefixes = new[] { "", "K", "M", "G", "T", "E", "Z" };
                var i = 0;

                while (bytes2 >= divisor && i < prefixes.Length - 1) {
                    bytes2 /= divisor;
                    i++;
                }

                var prefix = prefixes[i < prefixes.Length - 1 ? i : prefixes.Length - 1];

                return $"{bytes2:0.###} {prefix}{(Iec && prefix.Length > 0 ? "i" : "")}B";
            }
            throw new ArgumentException($"Type {typeof(T)} is not numeric");
        }

        public static bool IsNumericType(this Type o) => new List<Type>
            {
                typeof(int),
                typeof(long),
                typeof(uint),
                typeof(ulong),
                typeof(short),
                typeof(ushort),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(sbyte),
                typeof(byte)
            }.Contains(o);
        public static decimal KB<T>(this T kb) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => typeof(T).IsNumericType() ? Convert.ToDecimal(kb) * 1024 : throw new ArgumentException($"Type {typeof(T)} is not numeric");
        public static decimal MB<T>(this T mb) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => mb.KB() * 1024;
        public static decimal GB<T>(this T gb) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => gb.MB() * 1024;
        public static decimal TB<T>(this T tb) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => tb.GB() * 1024;
    }
}

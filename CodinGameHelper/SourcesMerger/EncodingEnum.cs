using System;
using System.Text;

namespace SourcesMerger
{
    public enum EncodingEnum
    {
        // ReSharper disable InconsistentNaming
        Default = 0,
        UTF8,
        ASCII,
        Unicode
        // ReSharper enable InconsistentNaming
    }

    public static class EncodingEnumExtensions
    {
        public static Encoding ToEncoding(this EncodingEnum encoding)
        {
            switch (encoding)
            {
                case EncodingEnum.Default: return Encoding.Default;
                case EncodingEnum.UTF8: return Encoding.UTF8;
                case EncodingEnum.ASCII: return Encoding.ASCII;
                case EncodingEnum.Unicode: return Encoding.Unicode;

                default: throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null);
            }
        }
    }

}
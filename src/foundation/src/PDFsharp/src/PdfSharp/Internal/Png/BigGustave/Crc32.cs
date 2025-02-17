﻿// PDFsharp - A .NET library for processing PDF
// See the LICENSE file in the solution root for more information.

// BigGustave is distributed with PDFsharp, but was published under a different license.
// See file LICENSE in the folder containing this file.

namespace PdfSharp.Internal.Png.BigGustave
{
    /// <summary>
    /// 32-bit Cyclic Redundancy Code used by the PNG for checking the data is intact.
    /// </summary>
    public static class Crc32
    {
        const uint Polynomial = 0xEDB88320;

        static readonly uint[] Lookup;

        static Crc32()
        {
            Lookup = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                var value = i;
                for (var j = 0; j < 8; ++j)
                {
                    if ((value & 1) != 0)
                    {
                        value = (value >> 1) ^ Polynomial;
                    }
                    else
                    {
                        value >>= 1;
                    }
                }

                Lookup[i] = value;
            }
        }

        /// <summary>
        /// Calculate the CRC32 for data.
        /// </summary>
        public static uint Calculate(byte[] data)
        {
            var crc32 = UInt32.MaxValue;
            for (var i = 0; i < data.Length; i++)
            {
                var index = (crc32 ^ data[i]) & 0xFF;
                crc32 = (crc32 >> 8) ^ Lookup[index];
            }

            return crc32 ^ UInt32.MaxValue;
        }

        /// <summary>
        /// Calculate the CRC32 for data.
        /// </summary>
        public static uint Calculate(List<byte> data)
        {
            var crc32 = UInt32.MaxValue;
            for (var i = 0; i < data.Count; i++)
            {
                var index = (crc32 ^ data[i]) & 0xFF;
                crc32 = (crc32 >> 8) ^ Lookup[index];
            }

            return crc32 ^ UInt32.MaxValue;
        }

        /// <summary>
        /// Calculate the combined CRC32 for data.
        /// </summary>
        public static uint Calculate(byte[] data, byte[] data2)
        {
            var crc32 = UInt32.MaxValue;
            for (var i = 0; i < data.Length; i++)
            {
                var index = (crc32 ^ data[i]) & 0xFF;
                crc32 = (crc32 >> 8) ^ Lookup[index];
            }

            for (var i = 0; i < data2.Length; i++)
            {
                var index = (crc32 ^ data2[i]) & 0xFF;
                crc32 = (crc32 >> 8) ^ Lookup[index];
            }

            return crc32 ^ UInt32.MaxValue;
        }
    }
}

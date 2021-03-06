﻿using System.IO;

namespace Koek
{
    public static class ExtensionsForBinaryReader
    {
        /// <summary>
        /// Reads a number of bytes and verifies that this many were indeed read.
        /// </summary>
        public static byte[] ReadBytesAndVerify(this BinaryReader instance, int count)
        {
            var bytes = instance.ReadBytes(count);

            if (bytes.Length != count)
                throw new EndOfStreamException();

            return bytes;
        }
    }
}
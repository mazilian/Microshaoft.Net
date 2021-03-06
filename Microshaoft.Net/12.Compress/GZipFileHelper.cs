﻿#if NET45
namespace Microshaoft
{
    using System;
    using System.IO;
    using System.IO.Compression;
    public static class GZipFileHelper
    {
        public static bool Decompress
                            (
                                string originalFileFullPath
                                , string targetDirectoryPath
                                , Func<string, string> onNamingDecompressedFileProcessFunc
                                , out string decompressedFileFullPath
                            )
        {
            var r = false;
            using (FileStream originalFileStream = File.OpenRead(originalFileFullPath))
            {
                var originalFileExtensionName = Path.GetExtension(originalFileFullPath);
                var originalDirectoryPath = Path.GetDirectoryName(originalFileFullPath);
                decompressedFileFullPath = PathFileHelper.GetNewPath(originalDirectoryPath, targetDirectoryPath, originalFileFullPath);
                string fileName = Path.GetFileName(decompressedFileFullPath);
                string directory = Path.GetDirectoryName(decompressedFileFullPath);
                if (onNamingDecompressedFileProcessFunc != null)
                {
                    fileName = onNamingDecompressedFileProcessFunc(fileName);
                }
                decompressedFileFullPath = Path.Combine(directory, fileName);
                using (FileStream decompressedFileStream = File.Create(decompressedFileFullPath))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        r = true;
                    }
                }
            }
            return r;
        }
    }
}
#endif
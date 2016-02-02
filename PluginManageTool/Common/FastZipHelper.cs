using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginManageTool.Common
{
    public class FastZipHelper
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="_localPath">待压缩的目录或文件</param>
        /// <param name="_zipFileName">压缩后的目录文件</param>
        public static void compress(string _localPath, string _zipFileName)
        {
            string localPath = _localPath;//待压缩的目录或文件
            string zipFileName = _zipFileName;//压缩后的目录文件
            // Export the directory tree from SVN server.
            //localPath = System.IO.Path.Combine(localPath, Guid.NewGuid().ToString());
            // Zip it into a memory stream.

            var zip = new ICSharpCode.SharpZipLib.Zip.FastZip();

            zip.CreateZip(zipFileName, localPath, true, string.Empty, string.Empty);
        }

        /// <summary>
        /// 解压
        /// </summary>
        public static void decompress(string _localPath, string _zipFileName)
        {
            string zipFileName = _zipFileName;//待解压的目录文件
            string localPath = _localPath;//解压后的目录
            // Zip it into a memory stream.
            var zip = new ICSharpCode.SharpZipLib.Zip.FastZip();
            zip.ExtractZip(zipFileName, localPath, string.Empty);
        }
    }
}

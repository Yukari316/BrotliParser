using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brotliparser
{
    internal class IOUtils
    {
        /// <summary>
        /// 通过包含文件或文件夹的路径数组来获取路径下的所有文件信息
        /// </summary>
        /// <param name="args">包含文件路径或文件夹路径的数组</param>
        public static List<FileInfo> GetAllFilesByArgs(string[] args)
        {
            int fileCount = 0;//文件计数
            int dirCount  = 0;//文件夹计数
            List<FileInfo> fileInfos = new List<FileInfo>();//用于存文件信息的LIST
            foreach (string arg in args)
            {
                if (IsFile(arg))//判断是否为文件
                {
                    fileInfos.Add(new FileInfo(arg));
                    fileCount++;
                }
                else
                {
                    dirCount++;
                    DirectoryIO    directory = new DirectoryIO(arg);
                    //获取文件夹下的文件并合并List
                    List<FileInfo> dirFileList  = directory.GetAllFileInfos();
                    fileInfos = fileInfos.Concat(dirFileList).ToList();
                    fileCount += directory.FileCount;
                    dirCount  += directory.DirCount;
                }
            }
            //Console.WriteLine($"File={fileCount},Dir={dirCount}");
            return fileInfos;
        }

        public static Func<string, bool> IsFile = (path) => File.Exists(path);
    }
}

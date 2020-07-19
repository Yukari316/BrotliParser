using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Tar;

namespace brotliparser
{
    internal class TarIO
    {
        /// <summary>
        /// 将所选文件夹进行打包
        /// 并在根目录下生成源文件夹的tar文件
        /// </summary>
        /// <param name="sourceDirectoryOrFilePath">源文件夹</param>
        public static void CreateTarFile(string sourceDirectoryOrFilePath)
        {
            
            List<FileInfo> files = new List<FileInfo>();
            if (!File.Exists(sourceDirectoryOrFilePath))//判断是否为单一文件
            {
                DirectoryIO sourceDirectory = new DirectoryIO(sourceDirectoryOrFilePath);
                files = sourceDirectory.GetAllFileInfos();
                if (files.Count == 0 && Directory.Exists(sourceDirectoryOrFilePath)) throw new IOException("目标文件夹为空");
            }
            else//参数为一个文件目录
            {
                FileInfo file = new FileInfo(sourceDirectoryOrFilePath);
                files.Add(file);
            }
            if(files.Count == 0) throw new IOException("未知错误(未读取到任何文件及文件夹)");

            //文件输出部分
            string outFileFullName = File.Exists(sourceDirectoryOrFilePath)
                                    ? $"{sourceDirectoryOrFilePath}.tar"
                                    : $"{sourceDirectoryOrFilePath.Substring(0, sourceDirectoryOrFilePath.Length - 1)}.tar";
            using (FileStream tarStream = new FileStream(outFileFullName, FileMode.Create))
            using (TarArchive tarOutputStream = TarArchive.CreateOutputTarArchive(tarStream))
            {
                foreach (FileInfo file in files)
                {
                    //将文件写入输出流
                    TarEntry tarEntry = TarEntry.CreateEntryFromFile(file.FullName);
                    //得到文件名或相对路径
                    tarEntry.Name = file.FullName.Equals(sourceDirectoryOrFilePath)
                                    ? file.Name
                                    : file.FullName.Substring(sourceDirectoryOrFilePath.Length);
                    tarOutputStream.WriteEntry(tarEntry,true);
                }
                tarOutputStream.Close();
            }
        }

        /// <summary>
        /// 解包TAR文件
        /// </summary>
        /// <param name="sourceFilePath">文件的路径</param>
        /// <param name="targetDirectoryPath">解包的输出路径</param>
        public static void DecompressTarFile(string sourceFilePath,string targetDirectoryPath)
        {
            //检查路径拼写
            if (!targetDirectoryPath.EndsWith("\\")) { targetDirectoryPath += "\\"; }
            using (TarInputStream tarFile = new TarInputStream(File.OpenRead(sourceFilePath)))
            {
                TarEntry tarEntry = null;
                //获取tar文件信息
                while ((tarEntry = tarFile.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToTar = tarEntry.Name;
                    if (!string.IsNullOrEmpty(pathToTar))
                    {
                        //获取文件夹名
                        directoryName = Path.GetDirectoryName(pathToTar) + "\\";
                    }
                    //直接创建文件夹由Directory判断是否创建
                    Directory.CreateDirectory(targetDirectoryPath + directoryName);

                    string fileName = Path.GetFileName(pathToTar);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        if (File.Exists(targetDirectoryPath + directoryName + fileName) || !File.Exists(targetDirectoryPath + directoryName + fileName))
                        {
                            using (FileStream outStream = File.Create(targetDirectoryPath + directoryName + fileName))
                            {
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    int bufSize = tarFile.Read(data, 0, data.Length);
                                    if (bufSize > 0) outStream.Write(data, 0, bufSize);
                                    else break;
                                }
                                outStream.Close();
                            }
                        }
                    }
                }
                tarFile.Close();
            }
        }
    }
}

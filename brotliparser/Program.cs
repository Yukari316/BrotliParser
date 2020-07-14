using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace brotliparser
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 压缩部分
            //TarIO.CreateTarFile(args[0]);
            // Console.ReadLine();
            // byte[]   tarBytes    = ByteArrayIO.File2Byte(args[0] + ".tar");
            // byte[]   brBytes     = BrotliUtils.BrotliCompress(tarBytes, 11);
            // string[] pathArg     = args[0].Split('\\');
            // string   savePath    = 
            //     string.Join("\\", pathArg.Take(pathArg.Length - 1).ToArray()) 
            //   + pathArg[pathArg.Length - 1];
            // ByteArrayIO.Bytes2File(brBytes, savePath + ".br");
            #endregion

            #region 解压部分
            // Console.WriteLine(File.Exists(@"Projects.br"));
            // byte[] brBytes = ByteArrayIO.File2Byte(@"Projects.br");
            // byte[] tarBytes = BrotliUtils.BrotliDecompress(brBytes);
            // ByteArrayIO.Bytes2File(tarBytes,@"out.tar");
            // brBytes = null;
            // tarBytes = null;
            // GC.Collect();
            // Console.ReadLine();
            #endregion

            #region Tar解包测试
            //TarIO.DecompressTarFile(args[0],args[1]);
            #endregion

            Console.ReadLine();
        }
    }
}

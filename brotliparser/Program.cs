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
            string[] fk = args.Skip(1).Take(args.Length - 1).ToArray();
            //List<FileInfo> fileInfos = IOUtils.GetAllFilesByArgs(args);
            foreach (string fileInfo in fk)
            {
                Console.WriteLine(fileInfo);
            }
            //Console.WriteLine(fileInfos.Count);
            Console.ReadLine();
//             byte[] brFile = IO.File2Byte("C:/Users/YBR_E/Desktop/redive_cn.db.br");
//             byte[] dbFileByte = BrotliUtils.Decode(brFile);
//             IO.Bytes2File(dbFileByte, "C:/Users/YBR_E/Desktop/redive_cn.db");
        }
    }
}

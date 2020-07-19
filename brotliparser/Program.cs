using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace brotliparser
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 4) return (int) ReturnType.IllegalArgs;

            #region 解压指令
            //BrotliDecompress
            //调用 -bd [输入文件路径] [输出路径] [保存文件名]
            if (args[0].Equals("-bd"))
            {
                //检查合法路径正则表达式
                Regex isDir = new Regex(@"^([a-zA-Z]:\\)?[^\/\:\*\?\""\<\>\|\,]*$");
                //检查合法文件名正则表达式
                Regex isFileName = new Regex(@"^[^\/\:\*\?\""\<\>\|\,]+$");
                if (
                    File.Exists(args[1]) &&                                                     //检查是否有文件存在
                    isDir.Match(Path.GetDirectoryName(args[2]) ?? string.Empty).Success &&//检查是否为合法路径
                    isFileName.Match(args[3] ?? string.Empty).Success)                          //检查文件名是否合法
                {
                    //在不存在路径时创建文件夹
                    Directory.CreateDirectory(Path.GetDirectoryName(args[2]) ?? string.Empty);
                    Console.WriteLine("读取文件...");
                    byte[] inputBuff;
                    try
                    {
                        //尝试读取文件
                        inputBuff = ByteArrayIO.File2Byte(args[1]);
                    }
                    catch (Exception e)
                    {
                        //读取文件发生错误
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"保存文件时发生错误\n{e}");
                        Thread.Sleep(3000);
                        return (int)ReturnType.IOError;
                    }
                    //读取到空文件
                    if (inputBuff.Length == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("读取到空文件");
                        return (int) ReturnType.EmptyFile;
                    }
                    byte[] outBuff;
                    try
                    {
                        Console.WriteLine("解压文件...");
                        //尝试解压文件数据
                        outBuff = BrotliUtils.BrotliDecompress(inputBuff);
                    }
                    catch (Exception e)
                    {
                        //解压发生错误
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"此文件不是Brotli压缩算法构建的无法解压\n{e}");
                        Thread.Sleep(3000);
                        return (int)ReturnType.ParseError;
                    }
                    try
                    {
                        Console.WriteLine("保存文件...");
                        //将文件保存到指定路径
                        ByteArrayIO.Bytes2File(outBuff,$"{args[2]}\\{args[3]}");
                    }
                    catch (Exception e)
                    {
                        //保存时发生错误
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"保存文件时发生错误\n{e}");
                        Thread.Sleep(3000);
                        return (int)ReturnType.IOError;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("无效参数");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(3000);
                    return (int)ReturnType.IllegalArgs;
                }
            }
            #endregion

            Console.ReadLine();
            return (int)ReturnType.Success;
        }
    }
}

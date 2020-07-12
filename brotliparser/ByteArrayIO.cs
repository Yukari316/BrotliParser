using System;
using System.IO;

namespace brotliparser
{
    internal class ByteArrayIO
    {
        #region 文件数组转换方法
        /// <summary>
        /// 将文件转换为byte数组
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] File2Byte(string path)
        {
            if (!File.Exists(path))
            {
                return new byte[0];
            }
            FileInfo fileInfo = new FileInfo(path);
            byte[] buff = new byte[fileInfo.Length];

            FileStream fileStream = fileInfo.OpenRead();
            fileStream.Read(buff, 0, Convert.ToInt32(fileStream.Length));
            fileStream.Close();

            return buff;
        }

        /// <summary>
        /// 将byte数组转换为文件并保存到指定地址
        /// </summary>
        /// <param name="buff">byte数组</param>
        /// <param name="savePath">保存地址</param>
        public static void Bytes2File(byte[] buff, string savePath)
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            FileStream fileStream = new FileStream(savePath, FileMode.CreateNew);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(buff, 0, buff.Length);
            binaryWriter.Close();
            fileStream.Close();
        }
        #endregion
    }
}

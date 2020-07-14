using Brotli;
using System;
using System.IO;

namespace brotliparser
{
    internal class BrotliUtils
    {
        /// <summary>
        /// 将压缩的byte数组解压
        /// </summary>
        /// <param name="input">需要解压缩的数据</param>
        /// <returns>解压的数据</returns>
        public static byte[] BrotliDecompress(byte[] input)
        {
            using (MemoryStream msInput = new MemoryStream(input))
            using (BrotliStream brotliStream = new BrotliStream(msInput, System.IO.Compression.CompressionMode.Decompress))
            using (MemoryStream msOutput = new MemoryStream())
            {
                brotliStream.CopyTo(msOutput);
                msOutput.Seek(0, SeekOrigin.Begin);
                Byte[] output = msOutput.ToArray();
                return output;
            }
        }

        /// <summary>
        /// 将byte数组压缩
        /// </summary>
        /// <param name="input">需要压缩的数据</param>
        /// <param name="quality">压缩等级[0-11]</param>
        /// <param name="window">压缩窗口大小[10-24]默认22</param>
        /// <returns>压缩的数据</returns>
        public static byte[] BrotliCompress(byte[] input,uint quality,uint window = 22)
        {
            using (MemoryStream msInput = new MemoryStream(input))
            using (MemoryStream msOutput = new MemoryStream())
            using (BrotliStream brotliStream = new BrotliStream(msOutput, System.IO.Compression.CompressionMode.Compress))
            {
                brotliStream.SetQuality(quality);
                brotliStream.SetWindow(window);
                msInput.CopyTo(brotliStream);
                brotliStream.Close();
                Byte[] output = msOutput.ToArray();
                return output;
            }
        }
    }

}

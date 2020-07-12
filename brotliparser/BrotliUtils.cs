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
        public static byte[] Decode(byte[] input)
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
        /// <returns>压缩的数据</returns>
        public static byte[] Encode(byte[] input)
        {
            using (MemoryStream msInput = new MemoryStream(input))
            using (MemoryStream msOutput = new MemoryStream())
            using (BrotliStream brotliStream = new BrotliStream(msOutput, System.IO.Compression.CompressionMode.Compress))
            {
                brotliStream.SetQuality(11);
                brotliStream.SetWindow(22);
                msInput.CopyTo(brotliStream);
                brotliStream.Close();
                Byte[] output = msOutput.ToArray();
                return output;
            }
        }
    }

}

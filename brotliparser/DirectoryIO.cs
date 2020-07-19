using System;
using System.Collections.Generic;
using System.IO;

namespace brotliparser
{
    internal class DirectoryIO
    {
        #region 属性
        /// <summary>
        /// 工作目录
        /// </summary>
        private string         Path      { set; get; }
        /// <summary>
        /// 获取到的所有文件列表
        /// </summary>
        private List<FileInfo> FileList { set; get; }
        /// <summary>
        /// 文件计数
        /// </summary>
        public  int            FileCount { set; get; }
        /// <summary>
        /// 文件夹计数
        /// </summary>
        public  int            DirCount { set; get; }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化实列
        /// </summary>
        /// <param name="path">工作目录</param>
        public DirectoryIO(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new NullReferenceException("指定了空路径");
            }
            Path = path;
            FileCount = 0;
            DirCount = 0;
            FileList = new List<FileInfo>();
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 获取指定目录下的所有文件
        /// </summary>
        public List<FileInfo> GetAllFileInfos()
        {
            if (string.IsNullOrEmpty(Path))
            {
                return null;
            }
            //清空原有的数据
            FileList.Clear();
            _Getallfiles(Path);
            return FileList.Count == 0 ? null : FileList;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 递归获取指定类型文件,包含子文件夹
        /// </summary>_FileList
        private void _Getallfiles(string path)
        {
            try
            {
                string[]      dir  = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);        //获取为文件夹的信息数组
                FileInfo[]    file = fdir.GetFiles();
                FileCount += file.Length;
                DirCount += dir.Length;
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo f in file) //添加当前目录所有文件到List
                    {
                        FileList.Add(f);
                    }
                    foreach (string d in dir)
                    {
                        _Getallfiles(d); //递归   
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"获取指定路径:{Path}失败，错误信息={ex.Message}" );
            }
        }

        #endregion
    }
}

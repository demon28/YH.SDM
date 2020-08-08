using System;
using System.Collections.Generic;
using System.Text;

namespace YH.SDM.Entity.Model
{
    /// <summary>
    /// 对文件上传响应模型
    /// </summary>
    public class UploadFileRequest
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }


    }
}

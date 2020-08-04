//----------------
//DA  v1.1
//2020-7-31
//Near
//---------------

using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace YH.EAM.Entity.CodeGenerator
{
    /// <summary>
    ///  文件上传表
    ///</summary>
    public class   Tsdm_uploadfile
    {

       public Tsdm_uploadfile()
       {
      
       }

        ///<summary>
        ///描述：主键id
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：原始文件名
        ///</summary>
        public string File_Encode_Name { get; set; }
        ///<summary>
        ///描述：服务器中文件名
        ///</summary>
        public string File_Decode_Name { get; set; }
        ///<summary>
        ///描述：文件大小
        ///</summary>
        public long File_Size { get; set; }
        ///<summary>
        ///描述：文件类型，后缀名
        ///</summary>
        public string File_Type { get; set; }
        ///<summary>
        ///描述：服务器中文件路径
        ///</summary>
        public string File_Path { get; set; }
        ///<summary>
        ///描述：上传人userid
        ///</summary>
        public int Upload_Userid { get; set; }
        ///<summary>
        ///描述：ip地址
        ///</summary>
        public string Upload_Ip { get; set; }
        ///<summary>
        ///描述：文件夹id
        ///</summary>
        public int Directory_Id { get; set; }
        ///<summary>
        ///描述：状态{0,正常，1,锁定}
        ///</summary>
        public int Status { get; set; }
        ///<summary>
        ///描述：逻辑删除
        ///</summary>
        public int Isdel { get; set; }
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime Create_Time { get; set; }
        ///<summary>
        ///描述：创建人id，外键user表
        ///</summary>
        public int Create_Userid { get; set; }
        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remarks { get; set; }

    }
 }









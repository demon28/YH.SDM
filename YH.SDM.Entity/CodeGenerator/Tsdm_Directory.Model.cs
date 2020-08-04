//----------------
//DA  v1.1
//2020-7-31
//Near
//---------------

using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace YH.EAM.Entity.CodeGenerator
{
    /// <summary>
    ///  文件夹表，（文件分类表）
    ///</summary>
    public class   Tsdm_directory
    {

       public Tsdm_directory()
       {
      
       }

        ///<summary>
        ///描述：主键id
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：文件夹名称
        ///</summary>
        public string Dir_Name { get; set; }
        ///<summary>
        ///描述：上级id
        ///</summary>
        public int ParentId { get; set; }
        ///<summary>
        ///描述：创建人id
        ///</summary>
        public int Create_Userid { get; set; }
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime Create_Time { get; set; }
        ///<summary>
        ///描述：状态{0,正常，1锁定}
        ///</summary>
        public int Status { get; set; }
        ///<summary>
        ///描述：逻辑删除{0,正常，1删除}
        ///</summary>
        public int Isdel { get; set; }
        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remarks { get; set; }


        [JsonIgnore]
        public Tsdm_directory Parent { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<Tsdm_directory> Childs { get; set; }


    }
 }









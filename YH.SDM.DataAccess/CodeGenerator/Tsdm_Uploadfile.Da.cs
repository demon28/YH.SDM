//DA  v1.1
//2020-7-31
//Near


using System.Collections.Generic;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.Entity.CodeGenerator;
using YH.SDM.Entity.Enums;
using YH.SDM.Entity.Model;

namespace YH.SDM.DataAccess.CodeGenerator
{

    /// <summary>
    ///   文件上传表
    ///</summary>
    public class Tsdm_uploadfile_Da : FreeSql.BaseRepository<Tsdm_uploadfile>
    {

        public Tsdm_uploadfile_Da() : base(DataAccess.DbContext.Db, null, null)
        {

        }

        /// <summary>
        /// 关键字搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<Tsdm_uploadfile> ListByWhere(string keyword, ref PageModel page) {

            var data =this.Select;

            if(!string.IsNullOrEmpty(keyword))
            {
               data= data.Where(s => s.File_Decode_Name.Contains(keyword));
            }

            page.TotalCount = data.Count().ToInt();
          

            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderBy(s => s.Create_Time)
                .ToList();

            return list;
        }

        /// <summary>
        /// 指定文件夹关键字搜索
        /// </summary>
        /// <param name="dirid"></param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UploadFileModel> ListByDirId(int dirid,string keyword, ref PageModel page) {

            var data = this.Orm.Select<Tsdm_uploadfile, Tsys_User,Tsdm_directory>()
                        .LeftJoin((tuf, tu,td) => tuf.Upload_Userid == tu.Id)
                        .LeftJoin((tuf, tu, td)=>tuf.Directory_Id==td.Id)
                        .Where((tuf, tu, td) => tuf.Directory_Id==dirid  && tuf.Isdel==(int)DelStatus.正常) ;


            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where((tuf, tu, td) => tuf.File_Decode_Name.Contains(keyword));
            }
           
            page.TotalCount = data.Count().ToInt(); 

            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderByDescending((tuf, tu, td) => tuf.Create_Time)
                .ToList((tuf, tu, td) => new UploadFileModel {  Upload_UserName=tu.Name , Upload_Directory=td.Dir_Name});

            return list;
        
        }



        public bool DeleteFiles(List<int> files_id) {

            var list = this.Select
                      .Where(s=> files_id.Contains(s.Id))
                       .ToList();

            int count = this.UpdateDiy.WhereDynamic(list).Set(a => a.Isdel, (int)DelStatus.删除).ExecuteAffrows();
            return count > 0;
        }

    }

}


//DA  v1.1
//2020-7-31
//Near


using System.Collections.Generic;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.Entity.CodeGenerator;
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

            var data = this.Orm.Select<Tsdm_uploadfile, Tsys_User>()
                        .LeftJoin((tuf, tu) => tuf.Upload_Userid == tu.Id)
                        .Where((tuf, tu) => tuf.Directory_Id==dirid);


            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where((tuf, tu) => tuf.File_Decode_Name.Contains(keyword));
            }

            
            page.TotalCount = data.Count().ToInt(); 


            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderByDescending((tuf, tu) => tuf.Create_Time)

                .ToList((tuf, tu) => new UploadFileModel {  Upload_UserName=tu.Name});

            return list;
        
        }


    }

}


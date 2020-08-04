//DA  v1.1
//2020-7-31
//Near


using System.Collections.Generic;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;

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


        public List<Tsdm_uploadfile> ListByDirId(int dirid) {

            return this.Select.Where(s => s.Directory_Id == dirid)
                .OrderByDescending(s => s.Create_Time)
                .ToList();
        
        }


    }

}


using System.Collections.Generic;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.SDM.DataAccess;
using YH.SDM.Entity.CodeGenerator;

namespace YH.EAM.DataAccess.CodeGenerator
{

    /// <summary>
    ///   
    ///</summary>
    public class Tsys_User_Da : FreeSql.BaseRepository<Tsys_User>
    {

        public Tsys_User_Da() : base(DbContext.Db, null, null)
        {

        }


        public List<Tsys_User> ListByWhere(string keyword, ref PageModel page) {

            var data =this.Select;

            if(!string.IsNullOrEmpty(keyword))
            {
               data= data.Where(s => s.Name.Contains(keyword) || s.Workid.Contains(keyword) );
            }

            page.TotalCount = data.Count().ToInt();
          

            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderBy(s => s.Createtime)
                .ToList();

            return list;
        }


    }

}


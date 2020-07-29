using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.SDM.DataAccess;
using YH.SDM.Entity.CodeGenerator;
using YH.SDM.Entity.Enums;
using YH.SDM.Entity.Model;
using YH.SDM.Entity;

namespace YH.SDM.DataAccess.CodeGenerator
{

    /// <summary>
    ///  
    ///</summary>
    public class Team_User_Da : FreeSql.BaseRepository<Team_User>
    {

        public Team_User_Da() : base(DataAccess.DbContext.Db, null, null)
        {

        }


        public List<Team_User> ListByWhere(string keyword, ref PageModel page)
        {

            var data = this.Select;

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(s => s.Name.Contains(keyword) || s.Workid.Contains(keyword));
            }

            page.TotalCount = data.Count().ToInt();


            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderBy(s => s.Comedate)
                .ToList();

            return list;
        }


    }

}


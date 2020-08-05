using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.DataAccess;
using YH.EAM.Entity.CodeGenerator;


namespace YH.SDM.DataAccess.CodeGenerator
{

    /// <summary>
    ///   
    ///</summary>
    public class Tsdm_customer_Da : FreeSql.BaseRepository<Tsdm_customer>
    {

        public Tsdm_customer_Da() : base(DataAccess.DbContext.Db, null, null)
        {

        }


        public List<Tsdm_customer> ListByWhere(string keyword, ref PageModel page) {

            var data =this.Select;

            if(!string.IsNullOrEmpty(keyword))
            {
             data= data.Where(s => s.Companychinese.Contains(keyword) || s.Companyenglish.Contains(keyword) );
            }

            page.TotalCount = data.Count().ToInt();
          

            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderBy(s => s.Createtime)
                .ToList();

            return list;
        }


    }

}


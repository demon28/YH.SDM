using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.SDM.DataAccess;
using YH.SDM.Entity.CodeGenerator;
using YH.SDM.Entity.Enums;

namespace YH.SDM.DataAccess.CodeGenerator
{

    /// <summary>
    ///  系统日志表
    ///</summary>
    public class Tsys_Log_Da : FreeSql.BaseRepository<Tsys_Log>
    {
        public Tsys_Log_Da() : base(DbContext.Db, null, null)
        {


        }


        public List<Tsys_Log> ListByWhere(string keyword, SysLogType type,DateTime? keybegindate, DateTime? keyenddate,ref PageModel page)
        {
        
            string sql = @$"select top {page.PageSize} * 
from (select row_number() 
over(order by id asc) as rownumber,* 
from [dbo].[Tsys_Log]) temp_row
where rownumber>(({page.PageIndex}-1)*  {page.PageSize})  ";


            string sql2 = "select count(0)  as [count]  from  [dbo].[Tsys_Log] ";

            if (!string.IsNullOrEmpty(keyword))
            {
          
                sql+= @" and Content like  ' %" + keyword + "%'";
                sql2 += @" and Content like  ' %" + keyword + "%'";
            }

            if (type != SysLogType.全部)
            { 
                sql += @" and  Type = " + type.ToInt64() + "";
                sql2+= @" and  Type = " + type.ToInt64() + "";
            }

            if (keybegindate.HasValue || keyenddate.HasValue)
            {
                sql += @" and  [Createtime] >='"+ keybegindate + "' and  [Createtime] <='"+ keyenddate + "'";
                sql2 += @" and  [Createtime] >='" + keybegindate + "' and  [Createtime] <='" + keyenddate + "'";
            }
            page.TotalCount = this.Orm.Ado.ExecuteDataTable(sql2).Rows[0]["count"].ToInt();

        
            var list = this.Select.WithSql(sql).ToList();
            return list;
        }





    }

}


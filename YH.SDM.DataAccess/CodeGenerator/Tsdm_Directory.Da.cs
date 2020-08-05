//DA  v1.1
//2020-7-31
//Near


using System.Collections.Generic;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.Entity.Enums;

namespace YH.SDM.DataAccess.CodeGenerator
{

    /// <summary>
    ///   文件夹表，（文件分类表）
    ///</summary>
    public class Tsdm_directory_Da : FreeSql.BaseRepository<Tsdm_directory>
    {

        public Tsdm_directory_Da() : base(DataAccess.DbContext.Db, null, null)
        {

        }

        /// <summary>
        /// 关键字查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<Tsdm_directory> ListByWhere(string keyword, ref PageModel page)
        {

            var data = this.Select;

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(s => s.Dir_Name.Contains(keyword));
            }

            page.TotalCount = data.Count().ToInt();


            var list = data.Page(page.PageIndex, page.PageSize)
                .OrderBy(s => s.Create_Time)
                .ToList();

            return list;
        }

        /// <summary>
        /// 树形菜单查询
        /// </summary>
        /// <returns></returns>
        public List<Tsdm_directory> ListByOderTree()
        {
            return Select.Where(s => s.Isdel == (int)DelStatus.正常).ToTreeList();
        }

        public bool DelDir(int id)
        {

            using (var uow = DataAccess.DbContext.Db.CreateUnitOfWork())
            {

                Tsdm_directory model = uow.Orm.Select<Tsdm_directory>().Where(s => s.Id == id).First();

                model.Isdel = (int)DelStatus.删除;

                int count = uow.Orm.Update<Tsdm_directory>(model).ExecuteAffrows(); ;


                if (count <= 0)
                {
                    uow.Rollback();
                    return false;
                }

                List<Tsdm_directory> list = uow.Orm.Select<Tsdm_directory>().Where(s => s.ParentId == id).AsTreeCte().ToList();


                foreach (var item in list)
                {
                    item.Isdel = (int)DelStatus.删除;
                }

                int offset= uow.Orm.Update<Tsdm_directory>(list).ExecuteAffrows();

                if (offset <= 0)
                {
                    uow.Rollback();
                    return false;
                }

                uow.Commit();
                return true;

            }
        }
    }

}


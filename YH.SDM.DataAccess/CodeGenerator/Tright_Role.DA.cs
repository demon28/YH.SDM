
using System;
using System.Collections.Generic;
using System.Linq;
using YH.SDM.Entity.CodeGenerator;

namespace YH.SDM.DataAccess.CodeGenerator
{

    /// <summary>
    ///  角色表
    ///</summary>
    public class Tright_Role_Da : FreeSql.BaseRepository<Tright_Role>
    {
        public Tright_Role_Da() : base(DataAccess.DbContext.Db, null, null)
        {


        }

    }

}


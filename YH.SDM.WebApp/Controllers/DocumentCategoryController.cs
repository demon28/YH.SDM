using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victory.Core.Controller;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.DataAccess.CodeGenerator;
using YH.SDM.WebApp.Attribute;

namespace YH.SDM.WebApp.Controllers
{
    [Authorize]
    public class DocumentCategoryController : TopControllerBase
    {
        public IActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public IActionResult List(string keyword, int pageIndex, int pageSize)
        {

            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;


            Tsdm_directory_Da da = new Tsdm_directory_Da();
            var list = da.ListByWhere(keyword, ref page);
            return SuccessResultList(list, page);
        }





    }
}

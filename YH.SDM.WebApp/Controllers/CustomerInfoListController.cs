using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victory.Core.Controller;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.DataAccess.CodeGenerator;

namespace YH.SDM.WebApp.Controllers
{
    public class CustomerInfoListController : TopControllerBase
    {
        public IActionResult InfoList()
        {
            return View();
        }

        public IActionResult List(string keywork,int pageIndex,int pageSize)
        {


            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;

            Tsdm_customer_Da da = new Tsdm_customer_Da();

            List<Tsdm_customer> list = da.ListByWhere(keywork, ref page);

            return SuccessResult(list);


        }



    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victory.Core.Controller;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.WebApp.Attribute;

namespace YH.SDM.WebApp.Controllers
{
    [Authorize]
    public class CustomerInfoInputController : TopControllerBase
    {
        public IActionResult InfoInput()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Tsdm_customer model)
        {

            DataAccess.CodeGenerator.Tsdm_customer_Da da = new DataAccess.CodeGenerator.Tsdm_customer_Da();
            da.Insert(model);

            return SuccessMessage();
        }
        
    }
}

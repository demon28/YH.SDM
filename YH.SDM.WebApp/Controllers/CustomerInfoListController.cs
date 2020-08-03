using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victory.Core.Controller;

namespace YH.SDM.WebApp.Controllers
{
    public class CustomerInfoListController : TopControllerBase
    {
        public IActionResult InfoList()
        {
            return View();
        }
    }
}

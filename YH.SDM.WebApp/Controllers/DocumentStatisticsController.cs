using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victory.Core.Controller;

namespace YH.SDM.WebApp.Controllers
{
    public class DocumentStatisticsController : TopControllerBase
    {
        public IActionResult Statistics()
        {
            return View();
        }
    }
}

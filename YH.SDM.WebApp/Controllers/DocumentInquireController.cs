using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victory.Core.Controller;

namespace YH.SDM.WebApp.Controllers
{
    public class DocumentInquireController : TopControllerBase
    {
        public IActionResult Inquire()
        {
            return View();
        }
    }
}

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
using YH.SDM.Entity.Enums;
using YH.SDM.WebApp.Attribute;

namespace YH.SDM.WebApp.Controllers
{

    [Authorize]
    public class DocumentUploadController : TopControllerBase
    {

        [Right(PowerName = "文档管理")]
        public IActionResult Index()
        {
            return View();
        }


        #region 文件管理

        public IActionResult Upload()
        {
            return View();
        }

        [Right(PowerName = "查询文件")]
        [HttpPost]
        public IActionResult ListFile(int dirid,string keyword, int pageIndex, int pageSize)
        {

            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;


            Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();
            var list = da.ListByDirId(dirid,keyword, ref page);
            return SuccessResultList(list, page);
        }

        [Right(PowerName = "添加文件")]
        [HttpPost]
        public IActionResult AddFile(Tsdm_uploadfile model)
        {

            Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();
            da.Insert(model);
            return SuccessMessage("添加成功！");

        }


        [Right(PowerName = "修改文件")]
        [HttpPost]
        public IActionResult UpdateFile(Tsdm_uploadfile model)
        {
            Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();
            da.Update(model);
            return SuccessMessage("成功！");
        }


        [Right(PowerName = "删除文件")]
        [HttpPost]
        public IActionResult DelFile(int id)
        {
            Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();

            if (da.Delete(s => s.Id == id) > 0)
            {
                return SuccessMessage("已删除！");

            }
            return FailMessage();
        }

    


        #endregion


        #region  文件夹管理



        [Right(PowerName = "添加文件夹")]
        [HttpPost]
        public IActionResult AddDir(Tsdm_directory model)
        {
            var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
            int  userId = int.Parse(userinfo.FindFirst("userId").Value);


            model.Create_Userid = userId;
            model.Create_Time = DateTime.Now;

            Tsdm_directory_Da da = new Tsdm_directory_Da();
            da.Insert(model);
            return SuccessMessage("添加成功！");

        }


        [Right(PowerName = "修改文件夹")]
        [HttpPost]
        public IActionResult UpdateDir(Tsdm_directory model)
        {
            Tsdm_directory_Da da = new Tsdm_directory_Da();
            da.Update(model);
            return SuccessMessage("成功！");
        }


        [Right(PowerName = "删除文件夹")]
        [HttpPost]
        public IActionResult DelDir(int id)
        {
            Tsdm_directory_Da da = new Tsdm_directory_Da();
            
            if (da.DelDir(id))
            {
                return SuccessMessage("已删除！");
            }
            return FailMessage();
        }



        [Right(PowerName = "查询文件夹")]
        [HttpPost]

        public IActionResult ListDirByTree()
        {

            Tsdm_directory_Da da = new Tsdm_directory_Da();
            var list = da.ListByOderTree();

            return SuccessResult(list);
        }



        #endregion




    }
}

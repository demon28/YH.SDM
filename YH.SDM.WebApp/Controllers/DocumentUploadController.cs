using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Victory.Core.Controller;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.DataAccess.CodeGenerator;
using YH.SDM.Entity.Enums;
using YH.SDM.Entity.Model;
using YH.SDM.WebApp.Attribute;

namespace YH.SDM.WebApp.Controllers
{

    [Authorize]
    public class DocumentUploadController : TopControllerBase
    {


        private static IWebHostEnvironment _hostingEnvironment;

        public DocumentUploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


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
        public IActionResult ListFile(int dirid, string keyword, int pageIndex, int pageSize)
        {

            PageModel page = new PageModel();
            page.PageIndex = pageIndex;
            page.PageSize = pageSize;


            Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();
            var list = da.ListByDirId(dirid, keyword, ref page);
            return SuccessResultList(list, page);
        }


        [HttpPost]
        public IActionResult AddFile(Tsdm_uploadfile model)
        {

            return View();

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


        [Right(PowerName = "上传文件")]
        [HttpPost]
        public IActionResult UploadFile(IFormCollection formCollection)
        {
            try
            {

                //获取系统根目录
                var webRootPath = _hostingEnvironment.WebRootPath;

                //拿到用户id
                var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
                int userId = int.Parse(userinfo.FindFirst("userId").Value);


                var uploadFileRequestList = new List<UploadFileRequest>();

                //FormCollection转化为FormFileCollection
                var files = (FormFileCollection)formCollection.Files;

                if (files.Count <= 0)
                {
                    return FailMessage("上传失败，未检测上传的文件信息");
                }


                //确定文件上传路径
                var filePath = "/FileUpload/Project/" + userId + "/";

                if (!Directory.Exists(webRootPath + filePath))
                {
                    Directory.CreateDirectory(webRootPath + filePath);
                }

                foreach (var formFile in files)
                {

                    //文件后缀
                    var fileExtension = Path.GetExtension(formFile.FileName);//获取文件格式，拓展名


                    //判断文件大小
                    var fileSize = formFile.Length;

                    //if (fileSize > 1024 * 1024 * 30) //10M TODO:(1mb=1024X1024b)
                    //{
                    //    return new JsonResult(new { isSuccess = false, resultMsg = "上传的文件不能大于30M" });
                    //}


                    //保存的文件名称(以名称和保存时间命名)
                    var saveName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.')) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;


                    //文件保存
                    using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        formFile.CopyTo(fs);
                        fs.Flush();
                    }


                    //完整的文件路径
                    var completeFilePath = Path.Combine(filePath, saveName);
                    uploadFileRequestList.Add(new UploadFileRequest()
                    {
                        FileName = saveName,
                        FilePath = completeFilePath
                    });
                }


                if (uploadFileRequestList.Any())
                {
                    return SuccessResult(uploadFileRequestList, "上传文件成功！");
                }

                return FailMessage();


            }
            catch (Exception ex)
            {
                return FailMessage("文件保存失败，异常信息为：" + ex.Message);
            }
        }


        #endregion


        #region  文件夹管理



        [Right(PowerName = "添加文件夹")]
        [HttpPost]
        public IActionResult AddDir(Tsdm_directory model)
        {
            var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
            int userId = int.Parse(userinfo.FindFirst("userId").Value);


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

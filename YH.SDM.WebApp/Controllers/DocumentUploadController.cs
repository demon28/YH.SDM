using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victory.Core.Controller;
using Victory.Core.Extensions;
using Victory.Core.Models;
using YH.EAM.Entity.CodeGenerator;
using YH.SDM.DataAccess.CodeGenerator;
using YH.SDM.Entity.CodeGenerator;
using YH.SDM.Entity.Enums;
using YH.SDM.Entity.Model;
using YH.SDM.WebApp.Attribute;

namespace YH.SDM.WebApp.Controllers
{

    [Authorize]
    public class DocumentUploadController : TopControllerBase
    {


        private static IWebHostEnvironment _hostingEnvironment;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DocumentUploadController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
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

            Tsdm_uploadfile model = da.Select.Where(s => s.Id == id).ToOne();

            model.Isdel = (int)DelStatus.删除;

            if (da.Update(model) > 0)
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


                var uploadtList = new List<Tsdm_uploadfile>();

                if (formCollection == null)
                {
                    return FailMessage("上传失败，未检测上传的文件信息");
                }

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


                    //保存的文件名称(以名称和保存时间命名)
                    //  var saveName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.')) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;

                    //将文件命名为gui,不存储为文件，数据库中保存真实文件名
                    var saveName = Guid.NewGuid().ToString();



                    //文件保存  TODO:未加密
                    using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        formFile.CopyTo(fs);
                        fs.Flush();
                    }


                    //完整的文件路径
                    var completeFilePath = Path.Combine(filePath, saveName);


                    uploadtList.Add(new Tsdm_uploadfile()
                    {
                        Create_Time = DateTime.Now,
                        Directory_Id = this.Request.Query.First().Value.ToInt(),
                        File_Decode_Name = formFile.FileName,
                        File_Encode_Name = saveName,
                        File_Number = string.Empty,
                        File_Path = filePath,
                        File_Size = fileSize,
                        File_Type = fileExtension,
                        Isdel = 0,
                        Status = 0,
                        Upload_Ip = this._httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),   //获取ip地址
                        Upload_Userid = userId,

                    });

                }

                Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();

                var list = da.Insert(uploadtList);

                if (list.Count > 0)
                {
                    return SuccessResult(uploadtList, "上传文件成功！");
                }

                return FailMessage();


            }
            catch (Exception ex)
            {
                return FailMessage("文件保存失败，异常信息为：" + ex.Message);
            }
        }

        [Right(PowerName = "下载文件")]
        public IActionResult DownLoadFile(int id)
        {

            Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();
            Tsdm_uploadfile model = da.Select.Where(s => s.Id == id).First();

            string url = _hostingEnvironment.WebRootPath + model.File_Path + model.File_Encode_Name;
            var stream = System.IO.File.OpenRead(url);

            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[model.File_Type];


            //增加系统操作日志
            var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
            var userName = userinfo.FindFirst("userName").Value;
            Tsys_Log_Da logda = new Tsys_Log_Da();

            logda.Insert(new Tsys_Log()
            {
                Content = $"用户[{userName}],下载文件：【{model.File_Decode_Name}】! 时间：{DateTime.Now}",
                Createtime = DateTime.Now,
                Type = (int)SysLogType.操作日志,
            });


            return File(stream, memi, model.File_Decode_Name);



        }

        [Right(PowerName = "批量下载")]
        public IActionResult BatchDownload(string ids)
        {
            string[] snumber= ids.Split(',');

            int[] idss = Array.ConvertAll(snumber, int.Parse);

            if (idss.Length <= 0)
            {
                return FailMessage("请选择您要下载的文件！");
            }

            //获取文件集合
            Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();
            List<Tsdm_uploadfile> files = da.Select.Where(s => idss.Contains(s.Id)).ToList();

            //获取系统根目录
            var webRootPath = _hostingEnvironment.WebRootPath;

            //拿到用户id
            var userinfo = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
            int userId = int.Parse(userinfo.FindFirst("userId").Value);
            string userName = userinfo.FindFirst("userName").Value;


            //创建并 确定文件===“下载”=====路径
            var fileDownLoadPath = "/FileDownload/Project/" + userId + "/";

            if (!Directory.Exists(webRootPath + fileDownLoadPath))
            {
                Directory.CreateDirectory(webRootPath + fileDownLoadPath);
            }

            string downloadurl = fileDownLoadPath + DateTime.Now.ToString("yyyyMMddhhmmss") + ".zip";

            StringBuilder sb = new StringBuilder();
            sb.Append($"用户{userName}下载如下文件：");

            using (ZipOutputStream zip = new ZipOutputStream(System.IO.File.Create(webRootPath + downloadurl)))
            {
                Crc32 crc = new Crc32();
                zip.SetLevel(1);  //不缺时间就用9，缺时间不缺空间用0.

                //打包压缩
                foreach (var file in files)
                {
                    sb.Append(file.File_Decode_Name);


                    string url = _hostingEnvironment.WebRootPath + file.File_Path + file.File_Encode_Name;  //拿出当前文件地址
                    var fs = System.IO.File.OpenRead(url);

                    byte[] buffer = new byte[fs.Length];

                    fs.Read(buffer, 0, buffer.Length);

                    ZipEntry entry = new ZipEntry(Path.GetFileName(file.File_Decode_Name))
                    {

                        DateTime = file.Create_Time,
                        Size = fs.Length,
                    };
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zip.PutNextEntry(entry);
                    zip.Write(buffer, 0, buffer.Length);

                }
            }

            //写系统操作日志
            Tsys_Log_Da logda = new Tsys_Log_Da();
            logda.Insert(new Tsys_Log()
            {
                Content = sb.ToString(),
                Createtime = DateTime.Now,
                Type = (int)SysLogType.操作日志,
            });

            return SuccessResult(downloadurl);

        }


        [Right(PowerName = "批量删除")]
        [HttpPost]
        public IActionResult BatchDelFile(List<int> ids)
        {
            if (ids.Count <= 0)
            {
                return FailMessage("请选择要删除的文件");
            }

            DataAccess.CodeGenerator.Tsdm_uploadfile_Da da = new Tsdm_uploadfile_Da();
            if (!da.DeleteFiles(ids))
            {
                return FailMessage("删除失败！");
            }

            return SuccessMessage("删除成功！");
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

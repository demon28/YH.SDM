using System;
using System.Collections.Generic;
using System.Text;
using YH.EAM.Entity.CodeGenerator;

namespace YH.SDM.Entity.Model
{
     public  class UploadFileModel: Tsdm_uploadfile
    {
        public string Upload_UserName { set; get; }
        public string Upload_Directory { get; set; }

    }
}

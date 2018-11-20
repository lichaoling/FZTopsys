using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Business.Common
{
    public class Enums
    {
        public class State
        {
            public static int Enable = 1;
            public static int Disable = 0;
        }

        public class MPUploadFileType
        {
            public string LXPW = "立项批文";
            public string YYZZ = "营业执照";
            public string TDCRHT = "土地出让合同";
            public string ZPMT = "总平面图";
            public string HXT = "红线图";
            public string WTS = "法定代表人委托书";
            public string JBRSFZ = "经办人身份证";
            public string DBRSFZ = "法定代表人身份证";
            public string QRCode = "二维码";
        }


    }
}
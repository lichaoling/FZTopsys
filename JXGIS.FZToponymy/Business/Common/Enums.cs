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

        public class HouseBZUploadFileType
        {
            public const string LXPW = "立项批文";
            public const string YYZZ = "营业执照";
            public const string TDCRHT = "土地出让合同";
            public const string ZPMT = "总平面图";
            public const string HXT = "红线图";
            public const string WTS = "法定代表人委托书";
            public const string JBRSFZ = "经办人身份证";
            public const string DBRSFZ = "法定代表人身份证";
            public const string QRCode = "二维码";
        }

        public class UploadFileCategory
        {
            public const string HouseBZ = "门牌号申请";
            public const string MPPic = "门牌照片";
            public const string MPQRCode = "门牌二维码";
            public const string RoadPic = "道路（桥梁）照片";
            public const string HousePic = "小区（楼宇）照片";
        }

    }
}
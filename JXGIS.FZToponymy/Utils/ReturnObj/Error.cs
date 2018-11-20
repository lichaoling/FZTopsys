using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Utils.ReturnObj
{
    public class Error : Exception
    {
        public Error(string message) : base(message)
        {
        }
    }
}
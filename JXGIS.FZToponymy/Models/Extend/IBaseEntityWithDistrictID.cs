using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Extend
{
    public interface IBaseEntityWithDistrictID
    {
        string DISTRICTID { get; set; }
    }
}
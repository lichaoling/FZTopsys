using JXGIS.FZToponymy.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Extend
{
    public interface IUser
    {
        string UserId { get; }

        string UserName { get; }

        string Department { get; }

        string Password { get; }
        string District { get; }
        List<string> DistrictIDList { get; }
    }
}
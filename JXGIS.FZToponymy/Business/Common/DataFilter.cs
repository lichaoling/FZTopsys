using JXGIS.FZToponymy.Models.Extend;
using JXGIS.FZToponymy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Business.Common
{
    public class DataFilter
    {
        public static IQueryable<T> DataFilterWithDist<T>(IQueryable<T> entity) where T : IBaseEntityWithDistrictID
        {
            // 先删选出当前用户权限内的数据
            if (LoginUtils.CurrentUser.DistrictIDList != null && LoginUtils.CurrentUser.DistrictIDList.Count > 0 && !LoginUtils.CurrentUser.DistrictIDList.Contains("福建省.福州市"))
            {
                var where = PredicateBuilder.False<T>();
                foreach (var userDID in LoginUtils.CurrentUser.DistrictIDList)
                {
                    where = where.Or(t => t.DISTRICTID.IndexOf(userDID + ".") == 0 || t.DISTRICTID == userDID);
                }
                entity = entity.Where(where);
            }
            return entity;
        }
    }
}
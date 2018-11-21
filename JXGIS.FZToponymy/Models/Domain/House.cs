using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("HOUSE")]
    public class HOUSE
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 所属行政区划ID
        /// </summary>
        public string DISTRICTID { get; set; }
        /// <summary>
        /// 小区（楼宇）名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 地址编码
        /// </summary>
        public string DZBM { get; set; }
        /// <summary>
        /// 地名地址分类码
        /// </summary>
        public string DZFLBM { get; set; }
        /// <summary>
        /// 小区（楼宇）简称
        /// </summary>
        public string SHORTNAME { get; set; }
        /// <summary>
        /// 小区（楼宇）别名
        /// </summary>
        public string ALIAS { get; set; }
        /// <summary>
        /// 使用状态
        /// </summary>
        public int STATE { get; set; }
        /// <summary>
        /// 门牌号范围
        /// </summary>
        public string MPNUMRANGE { get; set; }
        /// <summary>
        /// 小区（楼宇）照片
        /// </summary>
        public string ZPPATH { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 建成时间
        /// </summary>
        public DateTime JCSJ { get; set; }
        /// <summary>
        /// 占地面积
        /// </summary>
        public decimal ZDMJ { get; set; }
        /// <summary>
        /// 小区类型
        /// </summary>
        public string XQLX { get; set; }
        /// <summary>
        /// 功能
        /// </summary>
        public string GN { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal JZMJ { get; set; }
        /// <summary>
        /// 楼座数
        /// </summary>
        public int LZNUM { get; set; }
        /// <summary>
        /// 绿化率
        /// </summary>
        public string LHL { get; set; }
        /// <summary>
        /// 原地块名称
        /// </summary>
        public string YDKMC { get; set; }
        /// <summary>
        /// 地址方位
        /// </summary>
        public string DZFW { get; set; }
        /// <summary>
        /// 申报单位 
        /// </summary>
        public string SBDW { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LXR { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }
        /// <summary>
        /// 类型（点或面）
        /// </summary>
        public int TYPE { get; set; }
        /// <summary>
        /// 地名id
        /// </summary>
        public string TOPONUMYID { get; set; }
        /// <summary>
        /// 始建时间
        /// </summary>
        public DateTime SJSJ { get; set; }
        /// <summary>
        /// 名称含义
        /// </summary>
        public string MCHY { get; set; }
        /// <summary>
        /// 空间图形
        /// </summary>
        public string GEOM_WKT { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CREATETIME { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CREATEUSER { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LASTMODIFYTIME { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LASTMODIFYUSER { get; set; }
        /// <summary>
        /// 申请住宅门牌编制时间
        /// </summary>
        public DateTime SQBZTIME { get; set; }
        /// <summary>
        /// 申请住宅门牌编制人
        /// </summary>
        public string SQBZUSER { get; set; }
        /// <summary>
        /// 小区（楼宇）宣传名
        /// </summary>
        public string XCMC { get; set; }
        /// <summary>
        /// 小区（楼宇）登记名
        /// </summary>
        public string DJMC { get; set; }
        /// <summary>
        /// 地址全称(区县+镇街道+村社区+主道路+小区楼宇名）
        /// </summary>
        public string ADDRESS { get; set; }


        public virtual List<ROAD> ROADs { get; set; }
    }
}
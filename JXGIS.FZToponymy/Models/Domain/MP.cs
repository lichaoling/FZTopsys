using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("MP")]
    public class MP
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 所属道路ID
        /// </summary>
        public string ROADID { get; set; }
        /// <summary>
        /// 所属自然村ID
        /// </summary>
        public string VILIGEID { get; set; }
        /// <summary>
        /// 所属小区、楼宇ID
        /// </summary>
        public string HOUSEID { get; set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string MPNUM { get; set; }
        /// <summary>
        /// 门牌号码
        /// </summary>
        public string MPNUM_NO { get; set; }
        /// <summary>
        /// 地址全称(区县+镇街道+村社区+道路+门牌号+小区楼宇名、区县+镇街道+村社区+道路+门牌号、区县+镇街道+村社区+自然村+门牌号）
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// 地址编码（市辖区代码6位+镇街道代码3位+村社区代码3位+地址类型2位+门牌号顺序码5位+楼牌号顺序码3位+单元牌号顺序码3位+户室牌号顺序码4位）
        /// </summary>
        public string DZBM { get; set; }
        /// <summary>
        /// 地址二维码图片地址
        /// </summary>
        public string EWMPIC { get; set; }
        /// <summary>
        /// 地址生命状态
        /// </summary>
        public int STATE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 照片地址
        /// </summary>
        public string ZPPATH { get; set; }
        /// <summary>
        /// 编制时间
        /// </summary>
        public DateTime CREATETIME { get; set; }
        /// <summary>
        /// 编制人
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
        /// X坐标
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public string Y { get; set; }
        /// <summary>
        /// 门牌规格
        /// </summary>
        public string MPSIZE { get; set; }
       
    }
}
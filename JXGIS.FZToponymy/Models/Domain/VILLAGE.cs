using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("VILLAGE")]
    public class VILLAGE
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 所属行政区划ID
        /// </summary>
        public string DISTRICTID { get; set; }
        /// <summary>
        /// 自然村名称
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
        /// 自然村名简称
        /// </summary>
        public string SHORTNAME { get; set; }
        /// <summary>
        /// 自然村别名
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
        /// 自然村照片
        /// </summary>
        public string ZPPATH { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 类型（点或面）
        /// </summary>
        public int TYPE { get; set; }
        /// <summary>
        /// 空间图形
        /// </summary>
        public string GEOM_WKT { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREATETIME { get; set; }
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
        /// 申请农村门牌编制时间
        /// </summary>
        public DateTime SQBZTIME { get; set; }
        /// <summary>
        /// 申请农村门牌编制人
        /// </summary>
        public string SQBZUSER { get; set; }
    }
}
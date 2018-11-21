using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("ROAD")]
    public class ROAD
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 所属行政区id
        /// </summary>
        public string DISTRICTID { get; set; }
        /// <summary>
        /// 道路标准名称
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
        /// 道路类别
        /// </summary>
        public string DLLB { get; set; }
        /// <summary>
        /// 路线编码
        /// </summary>
        public string LXBM { get; set; }
        /// <summary>
        /// 道路简称
        /// </summary>
        public string SHORTNAME { get; set; }
        /// <summary>
        /// 道路别名
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
        /// 起点名称
        /// </summary>
        public string STARNAME { get; set; }
        /// <summary>
        /// 终点名称
        /// </summary>
        public string ENDNAME { get; set; }
        /// <summary>
        /// 道路照片
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
        /// 走向
        /// </summary>
        public string ZX { get; set; }
        /// <summary>
        /// 路面结构
        /// </summary>
        public string LMJG { get; set; }
        /// <summary>
        /// 名称含义
        /// </summary>
        public string MCHY { get; set; }
        /// <summary>
        /// 起止位置（东/南起）
        /// </summary>
        public string STARTDIRECTION { get; set; }
        /// <summary>
        /// 起止位置（西/北至）
        /// </summary>
        public string ENDDIRECTION { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public decimal LENGTH { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public decimal WIDTH { get; set; }
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
        public string TOPONYMYID { get; set; }
      
        /// <summary>
        /// 道路规划名
        /// </summary>
        public string PLANNAME { get; set; }
        /// <summary>
        /// 性质（快速路、主干道、次干道、大桥、内河桥梁）
        /// </summary>
        public string NATURE { get; set; }
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
        /// 申请道路门牌编制时间
        /// </summary>
        public DateTime SQBZTIME { get; set; }
        /// <summary>
        /// 申请道路门牌编制人
        /// </summary>
        public string SQBZUSER { get; set; }


        public virtual List<HOUSE> HOUSEs { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("HOUSEBZ")]
    public class HOUSEBZ
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 小区楼宇ID
        /// </summary>
        public string HOUSEID { get; set; }
        /// <summary>
        /// 申请编制时间
        /// </summary>
        public DateTime BZTIME { get; set; }
        /// <summary>
        /// 申报单位
        /// </summary>
        public string SBDW { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string APPLICANT { get; set; }
        /// <summary>
        /// 申请人联系方式
        /// </summary>
        public string APPLICANTPHONE { get; set; }

    }
}
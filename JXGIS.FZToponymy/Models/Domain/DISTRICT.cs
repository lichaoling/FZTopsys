using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("DISTRICT")]
    public class DISTRICT
    {
        [Key]
        public string DISTRICTID { get; set; }
        public string NAME { get; set; }
        public string PARENTID { get; set; }
        public string CODE { get; set; }
        public string GEOM_WKT { get; set; }

        public virtual DISTRICT Parent { get; set; }

        [ForeignKey("PARENTID")]
        public virtual List<DISTRICT> SubDistrict { get; set; }
        public virtual List<SYSUSER> SYSUSERs { get; set; }
    }
}
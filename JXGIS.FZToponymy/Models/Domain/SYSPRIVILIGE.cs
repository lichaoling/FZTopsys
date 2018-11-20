using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("SYSPRIVILIGE")]
    public class SYSPRIVILIGE
    {
        [Key]
        public string PRIVILIGEID { get; set; }

        public string CONTROLLER { get; set; }

        public string CONTROLLERNAME { get; set; }

        public string ACTION { get; set; }

        public string ACTIONNAME { get; set; }

        public string DESCIRPTION { get; set; }

        public string TYPE { get; set; }

        public virtual List<SYSUSER> SYSUSERs { get; set; }

        public virtual List<SYSROLE> SYSROLEs { get; set; }

        public bool NEEDAUTHORIZE { get; set; }

        public DateTime? CREATETIME { get; set; }

        public DateTime? LASTMODIFYTIME { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("SYSROLE")]
    public class SYSROLE
    {
        [Key]
        public string ROLEID { get; set; }

        public string ROLENAME { get; set; }

        public string ROLEDESCRIPTION { get; set; }

        public DateTime? CREATETIME { get; set; }

        public DateTime? LASTMODIFYTIME { get; set; }

        public virtual List<SYSUSER> SYSUSERs { get; set; }

        public virtual List<SYSPRIVILIGE> SYSPRIVILIGEs { get; set; }
    }
}
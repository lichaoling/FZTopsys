using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("SYSDEPARTMENT")]
    public class SYSDEPARTMENT
    {
        [Key]
        public string DEPARTMENTID { get; set; }

        public string PARENTID { get; set; }

        public string NAME { get; set; }

        public string DESCRIPTION { get; set; }

        public DateTime? CREATETIME { get; set; }

        public DateTime? LASTMODIFYTIME { get; set; }

        public virtual SYSDEPARTMENT Parent { get; set; }

        [ForeignKey("PARENTID")]
        public virtual List<SYSDEPARTMENT> SubDepartments { get; set; }

        public virtual List<SYSUSER> SYSUSERs { get; set; }
    }
}
using JXGIS.FZToponymy.Models.Extend;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("SYSUSER")]
    public class SYSUSER : IUser
    {
        [Key]
        public string USERID { get; set; }

        public string USERNAME { get; set; }

        public string PASSWORD { get; set; }

        public string NAME { get; set; }

        public string SEX { get; set; }

        public string EMAIL { get; set; }

        public string TELEPHONE { get; set; }

        public bool PASSREVIEW { get; set; } = false;

        public DateTime? BIRTHDAY { get; set; }

        public DateTime? CREATETIME { get; set; }

        public DateTime? LASTMODIFYTIME { get; set; }

        public virtual List<SYSROLE> SYSROLEs { get; set; }

        public virtual List<SYSPRIVILIGE> SYSPRIVILIGEs { get; set; }

        public virtual List<SYSDEPARTMENT> SYSDEPARTMENTs { get; set; }

        public virtual List<DISTRICT> DISTRICTs { get; set; }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        [NotMapped]
        public bool IsAdmin
        {
            get
            {
                return Priviliges.Any(p => p.CONTROLLER.ToUpper() == "ADMIN" && p.ACTION.ToUpper() == "INDEX");
            }
        }

        /// <summary>
        /// 用户所有的权限
        /// </summary>
        [NotMapped]
        public List<SYSPRIVILIGE> Priviliges
        {
            get
            {
                List<SYSPRIVILIGE> priviliges = new List<SYSPRIVILIGE>();
                if (this.SYSROLEs != null)
                {
                    foreach (var role in this.SYSROLEs)
                    {
                        priviliges.AddRange(role.SYSPRIVILIGEs);
                    }
                }

                if (this.SYSPRIVILIGEs != null)
                {
                    priviliges.AddRange(this.SYSPRIVILIGEs);
                }
                return priviliges;
            }
        }

        [NotMapped]
        public string UserId
        {
            get
            {
                return this.USERNAME;
            }
        }

        [NotMapped]
        public string UserName
        {
            get
            {
                return this.NAME;
            }
        }

        [NotMapped, JsonIgnore]
        public string Password
        {
            get
            {
                return this.PASSWORD;
            }
        }

        [NotMapped]
        public string Department
        {
            get
            {

                string s = string.Empty;
                if (this.SYSDEPARTMENTs != null)
                {
                    char split = '|';
                    foreach (var d in this.SYSDEPARTMENTs)
                    {
                        s += d.NAME + split;
                    }
                    s = s.Trim(split);
                }
                return s;
            }
        }

        [NotMapped]
        public string District
        {
            get
            {

                string s = string.Empty;
                if (this.DISTRICTs != null)
                {
                    char split = '|';
                    foreach (var d in this.DISTRICTs)
                    {
                        s += d.NAME + split;
                    }
                    s = s.Trim(split);
                }
                return s;
            }
        }
        [NotMapped]
        public List<string> DistrictIDList
        {
            get
            {
                List<string> s = null;
                if (this.DISTRICTs != null)
                {
                    s = this.DISTRICTs.Select(t => t.DISTRICTID).ToList();
                }
                return s;
            }
        }

    }
}
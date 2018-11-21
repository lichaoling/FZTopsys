using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("DMOFUPLOADFILES")]
    public class DMOFUPLOADFILES
    {
        [Key]
        public string ID { get; set; }
        public string DMID { get; set; }
        public string FILENAME { get; set; }
        public string FILEEX { get; set; }
        public string TYPE { get; set; }
        public int STATE { get; set; }
    }
}
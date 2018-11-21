using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Models.Domain
{
    [Table("HOUSEBZOFUPLOADFILES")]
    public class HOUSEBZOFUPLOADFILES
    {
        [Key]
        public string ID { get; set; }
        public string HOUSEBZID { get; set; }
        public string FILENAME { get; set; }
        public string FILEEX { get; set; }
        public string TYPE { get; set; }
        public int STATE { get; set; }
    }
}
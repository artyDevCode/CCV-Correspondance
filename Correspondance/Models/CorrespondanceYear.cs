using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CCVCorrespondance.Models
{
    public class CorrespondanceYear
    {
        [Key]
        public int ID { get; set; }
        
        //[ForeignKey("CorrespondanceType")]
        //public string CorrespondanceTypeName { get; set; }

        public int CorrespondanceYearNumber { get; set; }
    }
}
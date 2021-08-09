using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCVCorrespondance.Models
{
    public class CorrespondanceType
    {
        [Key]
        public int ID { get; set; }

        //[ForeignKey("Correspondance")]
        //public int CorrespondanceID { get; set; }
        //public virtual Correspondance Correspondance { get; set; }
        public string CorrespondanceTypeName { get; set; }

        public virtual ICollection<CorrespondanceYear> correspondanceyear {get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCVCorrespondance.Models
{
    public class Correspondance
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[Display(Name = "Type")]
        //[ForeignKey("CorrespondanceType")]
        //public int CorrespondanceTypeID { get; set; }
        //public virtual CorrespondanceType CorrespondanceType { get; set; }

        [StringLength(10)]
        [Display(Name = "Type")]
        public string CorrespondanceType { get; set; }

        [Display(Name = "Year")]
        public int CorrespondanceYear { get; set; }

        [StringLength(500)]
        [Display(Name = "From/To")]
        [Required(ErrorMessage = "Please enter the correspondance name")]
        public string CorrespondanceFromTo { get; set; }
        
        [Display(Name = "Department")]
        [StringLength(500)]
        public string CorrespondanceDepartment { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Please enter a correspondance subject")]
        [Display(Name = "Subject")]
        public string CorrespondanceSubject { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string CorrespondanceDescription { get; set; }

        [DataType(DataType.Date)]
        //[DateRange("2010/12/01", "2010/12/16")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date On Letter")]
        public DateTime? CorrespondanceDateOnLetter { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Received / Sent")]
        public DateTime CorrespondanceDateReceivedOrSent { get; set; }
        
        [AllowHtml]
        //[Required(ErrorMessage = "Please enter a correspondance body")]
        [Display(Name = "Body")]
        public string CorrespondanceBody { get; set; }
        
        [AllowHtml]
        [Display(Name = "Additional Comments")]
        public string CorrespondanceAdditionalComments { get; set; }
        
        [Display(Name = "Date Modified")]
        public DateTime DocumentDateModified { get; set; }

        [Display(Name = "Modified By")]
        public string DocumentModifiedBy { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DocumentDateCreated { get; set; }

        [Display(Name = "Created By")]
        public string DocumentCreatedBy { get; set; }
        
        public bool DocumentDeleted { get; set; }

        public virtual ICollection<CorrespondanceType> collectionType { get; set; }
    }
}
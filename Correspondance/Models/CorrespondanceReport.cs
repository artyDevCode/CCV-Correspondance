using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CCVCorrespondance.Models
{
    public class CorrespondanceReport
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "User")]
        public string CorrespondanceReportUser { get; set; }

        [Display(Name = "Start Date")]
        public string CorrespondanceReportStartDate { get; set; }
        
        [Display(Name = "End Date")]
        public string CorrespondanceReportEndDate { get; set; }

        [Display(Name = "Correspondance Type")]
        public string CorrespondanceReportTypeName { get; set; }

        [Display(Name = "Received/Sent Date")]
        public DateTime CorrespondanceReportReceivedOrSentDate { get; set; }
        
        [Display(Name = "From/To")]
        public string CorrespondanceReportFromTo { get; set; }
        
        [Display(Name = "Department")]
        public string CorrespondanceReportDepartment { get; set; }
        
        [Display(Name = "Subject")]
        public string CorrespondanceReportSubject { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCVCorrespondance.Models
{
    public class CorrespondanceVM
    {
        public int VM_Id { get; set; }
        public string VM_Type { get; set; }
        public int VM_Year { get; set; }
        public string VM_FromTo { get; set; }
        public string VM_Department { get; set; }
        public string VM_Subject { get; set; }
        public DateTime? VM_DateOnLetter { get; set; }
        public DateTime VM_DateReceivedOrSent { get; set; }

    }

    public class CorrespondanceSubjectVM
    {
        public int VM_Id { get; set; }
        public string VM_Subject { get; set; }
        public string VM_Type { get; set; }
        public int VM_Year { get; set; }
        public string VM_FromTo { get; set; }
        public string VM_Department { get; set; }
        public DateTime? VM_DateOnLetter { get; set; }
        public DateTime VM_DateReceivedOrSent { get; set; }

    }

    public class CorrespondanceDepartmentVM
    {
        public int VM_Id { get; set; }
        public string VM_Subject { get; set; }
        public string VM_Type { get; set; }
        public int VM_Year { get; set; }
        public string VM_FromTo { get; set; }
        public string VM_Department { get; set; }
        public DateTime? VM_DateOnLetter { get; set; }
        public DateTime VM_DateReceivedOrSent { get; set; }
    }

    public class CorrespondanceDateVM
    {
        public int VM_Id { get; set; }
        public DateTime? VM_DateOnLetter { get; set; }
        public string VM_Type { get; set; }
        public int VM_Year { get; set; }
        public string VM_FromTo { get; set; }
        public string VM_Subject { get; set; }
        public string VM_Department { get; set; }
        public DateTime VM_DateReceivedOrSent { get; set; }
    }

    public class CorrespondanceFromToVM
    {
        public int VM_Id { get; set; }
        public string VM_FromTo { get; set; }
        public string VM_Type { get; set; }
        public int VM_Year { get; set; }
        public string VM_Department { get; set; }
        public string VM_Subject { get; set; }
        public DateTime? VM_DateOnLetter { get; set; }
        public DateTime VM_DateReceivedOrSent { get; set; }
    }

}
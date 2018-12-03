﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AdmissionSys.Models
{
   
    public class ApplicationList
    {
        public int ApplicationListID { get; set; }

        [Display(Name = "Application Category")]
        [Required(ErrorMessage = "Please Specify a category to apply from")]
        public string ApplicationCategory { get; set; }

        [Required]
        public string Status { get; set; }
        public string PrioAreaOfResearch { get; set; }

        [ScaffoldColumn(false)]
        public bool AttendInterview { get; set; }

        [ScaffoldColumn(false)]
        public bool ConfirmFeesPayment { get; set; }

        [ScaffoldColumn(false)]
        public bool CounsellingDone { get; set; }

        [ScaffoldColumn(false)]
        public bool AdmissionConfirmed { get; set; }

        [ScaffoldColumn(false)]
        public bool AcademicRecAdded { get; set; }

        [ScaffoldColumn(false)]
        public bool FormVerified { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string ProgramsID { get; set; }

        public Student Student { get; set; }
        public Programs Programs { get; set; }

    }
}

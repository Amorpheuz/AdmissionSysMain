using System;
using System.ComponentModel.DataAnnotations;

namespace AdmissionSys.Models
{
public enum ApplicationCat
    {
        General,
        SC,
        ST,
        OBC,
        Management,
        Foreigner,
        SportsQuota
    }
    public class ApplicationList
    {
        public int ApplicationListID { get; set; }

        [Display(Name = "Application Category")]
        [Required(ErrorMessage = "Please Specify a category to apply from")]
        public ApplicationCat? ApplicationCat { get; set; }

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
        public bool DocumentUploaded { get; set; }

        [ScaffoldColumn(false)]
        public bool FormVerified { get; set; }

        public DateTime LastOpr { get; set; }

        public string ProgramsID { get; set; }

        public int StudentID { get; set; }

        public Student Student { get; set; }
        public Programs Programs { get; set; }

    }
}

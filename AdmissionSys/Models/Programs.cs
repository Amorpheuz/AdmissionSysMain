using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionSys.Models
{
    public class Programs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProgramsID { get; set; }

        [Required(ErrorMessage = "Please Specify Program Name")]
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }

        public bool IsIntakeOpen { get; set; }

        public string DocsRequired { get; set; } 

        public ICollection<AcademicYear> AcademicYears { get; set; }
        public ICollection<Fees> Fees { get; set; }
    }
}
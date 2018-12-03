using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionSys.Models
{
    public class Programs
    {
        public string ProgramsID { get; set; }

        [Required(ErrorMessage = "Please Specify Program Name")]
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }

        [Required(ErrorMessage = "Please specify if the intake is still open?")]
        [Display(Name = "Is intake open?")]
        public bool IsIntakeOpen { get; set; }

        [Display(Name = "Documents required to be uploaded before applying.")]
        public string DocsRequired { get; set; }

        [Required(ErrorMessage = "Please Specify the intake capacity for this program!")]
        [Display(Name = "Intake Capacity")]
        public int IntakeCapacity { get; set; }

        [Required(ErrorMessage = "Please Specify Minimum Elegibility Criteria for the program")]
        [Display(Name = "Elegibility Criteria")]
        public int ElegibilityCriteria { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<ApplicationList> ApplicationLists { get; set; }
        public ICollection<Fees> Fees { get; set; }
    }
}
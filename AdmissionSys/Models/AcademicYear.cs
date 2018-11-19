using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdmissionSys.Models
{
    public class AcademicYear
    {
        public int AcademicYearID { get; set; }

        [Required(ErrorMessage = "Please select an academic year")]
        [Display(Name = "Academic Year")]
        [DataType(DataType.Date)]
        public DateTime AcaYear { get; set; }

        [Required(ErrorMessage = "Please specify end date for the academic year")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date for Academic Year")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please Specify Start date for academic year")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please Specify intake capacity for specified program")]
        [Display(Name = "Intake Capacity for the Program")]
        public int IntakeCapacity { get; set; }

        public Programs Programs { get; set; }

        public ICollection<ApplicationList> ApplicationLists { get; set; }

    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace AdmissionSys.Models
{
    public class Fees
    {
        public int FeesID { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please Specify Fees Amount")]
        [Display(Name = "Fees Amount")]
        public int FeesAmount { get; set; }

        [Required(ErrorMessage = "Please Specify Fees Type for the program")]
        [Display(Name = "Fees Type")]
        public string FeesType { get; set; }

        public string ProgramsID { get; set; }

        public Program Program { get; set; }
    }
}
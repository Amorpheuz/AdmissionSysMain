using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace AdmissionSys.Models
{
    public enum Gender
    {
          Male, Female, Other
    }

    public enum Caste
    {
      General, SC, ST, OBC, Other
    }
    public enum RelationWithGuardian
    {
       Mother, Father, Uncle , Aunt, Other
    }
    public enum BloodGroup
    {
        [Display(Name = "A Positive")]
        APos,

        [Display(Name = "A Negative")]
        ANeg,

        [Display(Name = "B Positive")]
        BPos,

        [Display(Name = "B Negative")]
        BNeg,

        [Display(Name = "AB Positive")]
        ABPos,

        [Display(Name = "AB Negative")]
        ABNes,

        [Display(Name = "O Positive")]
        OPos,

        [Display(Name = "O Negative")]
        ONes
    }

    public enum State
    {
        [Display(Name = "Andhra Pradesh")]
        AndhraPradesh,

        [Display(Name = "Arunachal Pradesh")]
        ArunachalPradesh,

        Assam,
        Bihar,
        Chhattisgarh,
        Goa,
        Gujarat,
        Haryana,

        [Display(Name = "Himachal Pradesh")]
        HimachalPradesh,

        [Display(Name = "Jammu And Kashmir")]
        JammuAndKashmir,

        Jharkhand,
        Karnataka,
        Kerala,

        [Display(Name = "Madhya Pradesh")]
        MadhyaPradesh,

        Maharashtra,
        Manipur,
        Meghalaya,
        Mizoram,
        Nagaland,
        Odisha,
        Punjab,
        Rajasthan,
        Sikkim,

        [Display(Name = "Tamil Nadu")]
        TamilNadu,

        Telangana,
        Tripura,

        [Display(Name = "Uttar Pradesh")]
        UttarPradesh,

        Uttarakhand,
        WestBengal,

        [Display(Name = "Andaman and Nicobar Islands")]
        AndamanAndNicobar,

        Chandigarh,

        [Display(Name = "Dadar and Nagar Haveli")]
        Dadar,

        [Display(Name = "Daman And Diu")]
        Daman,

        Delhi,
        Lakshadweep,
        Puducherry

    }
    
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Please Specify Your Name"), StringLength(60)]
        [Display(Name = "Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please Specify Your Address"), StringLength(500, ErrorMessage = "Such long Address! Please specify a shorter address with less that 300 characters, our database run out of space!")]
        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string StudentAddress { get; set; }

        [Required(ErrorMessage = "Please Specify Your City")]
        [Display(Name = "City")]
        [StringLength(25, ErrorMessage = "City should not have more than 25 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Specify Your Postal Code")]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Please Specify Your State")]
        [Display(Name = "State")]
        public State? State { get; set; }

        [Required(ErrorMessage = "Please Specify Your Nationality")]
        [Display(Name = "Nationality")]
        [StringLength(25, ErrorMessage = "Nationality should not have more than 25 characters")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Please Specify Your Gender")]
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; } //make combobox

        [Required, StringLength(30, ErrorMessage = "Guardian Name can not be more than 30 characters long")]
        [Display(Name = "Guardian Name")]
        public string StudentGuardianName { get; set; }

        [Required(ErrorMessage = "Please Specify Your Relation with the Guardian")]
        [Display(Name = "What's your relation with guardian you specified")]
        public RelationWithGuardian? RelationWithGuardian { get; set; } //make combobox

        [Required(ErrorMessage = "Please Specify Your Blood Group")]
        [Display(Name = "Blood Group")]
        public BloodGroup? BloodGroup { get; set; } //make combobox

        [Required(ErrorMessage = "Please Specify Your Date Of Birth")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Residence Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ResidencePhone { get; set; }

        [Required(ErrorMessage = "Please specify your date of birth")]
        [Display(Name = "Place Of Birth")]
        [StringLength(25, ErrorMessage = "Place Of Birth should not have more than 25 characters")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Guardian's Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Required(ErrorMessage = "Please specify guardian's contact number")]
        public string GuardianMobileNumber { get; set; }

        [Display(Name = "Your Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Required(ErrorMessage = "Please specify your contact number")]
        [DataType(DataType.PhoneNumber)]
        public string ApplicantsMobileNumber { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Family Income")]
        public decimal FamilyIncome { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Your Signature")]
        [Required(ErrorMessage = "Please Upload Your Signature")]
        public string StudentSignature { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Your Photo")]
        [Required(ErrorMessage = "Please Upload Your Photo")]
        public string StudentPhoto { get; set; }

        [Display(Name = "Aadhaar Number")]
        [RegularExpression(@"^\d{4}\s\d{4}\s\d{4}\s\d{4}$", ErrorMessage = "Not A Valid Aadhar Number")]
        public string AadharNumber { get; set; }

        [Display(Name = "Caste")]
        public Caste? Caste { get; set; } //make combobox

        [Display(Name = "Occupation of Guardian")]
        [StringLength(20, ErrorMessage = "Just Specify Guardian Occupation, please don't describe it")]
        public string GuardianOccupation { get; set; }

        [Required(ErrorMessage = "Physically Challenged Status is required")]
        [Display(Name = "Are you Physically Challenged? Tick for yes.")]
        public bool PhysicallyHandicapStatus { get; set; } //make checkbox

        [ScaffoldColumn(false)]
        public bool FillPersonalInfo { get; set; }

        [ScaffoldColumn(false)]
        public string PersonalMessage { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NotMapped]
        [Display(Name = "Upload Your Photo")]
        //[Required(ErrorMessage = "Please Upload Your Photo")]
        public IFormFile StudentPhotoActual { get; set; }

        [NotMapped]
        [Display(Name = "Upload Your Signature")]
        //[Required(ErrorMessage = "Please Upload Your Signature")]
        public IFormFile StudentSignatureActual { get; set; }

        public string userID { get; set; }

        public ICollection<Documents> Documents { get; set; }
        public ICollection<AcademicRecord> AcademicRecords { get; set; }
        public ICollection<ApplicationList> ApplicationLists { get; set; }
    }
}
